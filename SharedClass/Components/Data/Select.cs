using SharedClass.Components.Model;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Data;
using Dapper;
using System.Reflection;
using BoldReports.Writer;
using BoldReports.Web;

namespace SharedClass.Components.Data
{
    public class Select : CRUD
    {
        private readonly SqlConnection con;

        public Select()
        {
            con = GetSqlConnection();
        }

        public async Task<IEnumerable<ItemClass>> GetItemsAsync()
        {
            return await con.QueryAsync<ItemClass>("SELECT * FROM Items ORDER BY ItemCode");
        }

        public async Task<IEnumerable<ItemClass>> GetItemsAsync(string ItemType)
        {
            return await con.QueryAsync<ItemClass>("SELECT * FROM Items WHERE ItemType = @ItemType ORDER BY ItemCode", new { ItemType });
        }

        public static DataTable ConvertListToDataTable(List<string> list)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Value", typeof(string));

            foreach (string s in list)
            {
                table.Rows.Add(s);
            }

            return table;
        }

        public static DataTable ConvertIntToDataTable(List<int> list)
        {
            DataTable table = new DataTable();
            table.Columns.Add("RowID", typeof(int));

            foreach (int s in list)
            {
                table.Rows.Add(s);
            }

            return table;
        }

        public async Task<IEnumerable<Stock>> GetStockDataAsync(string ItemID = null)
        {
            var query = "SELECT * FROM Stock";

            if (ItemID != null)
            {
                query += " WHERE ItemID = @ItemID";
            }

            query += " ORDER BY ItemID";

            return await con.QueryAsync<Stock>(query, new { ItemID });
        }

        public bool IsValidJson(string input)
        {
            input = input.Trim();
            if ((input.StartsWith("{") && input.EndsWith("}")) ||
                (input.StartsWith("[") && input.EndsWith("]")))
            {
                try
                {
                    JsonDocument.Parse(input);
                    return true;
                }
                catch (JsonException)
                {
                    return false;
                }
            }
            return false;
        }
        public static DataTable ConvertToDataTable<T>(List<T> list)
        {
            DataTable table = new DataTable();

            // Get the properties of the type
            PropertyInfo[] properties = typeof(T).GetProperties();

            // Create the columns in the DataTable based on the properties
            foreach (PropertyInfo property in properties)
            {
                table.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            // Populate the DataTable with data from the list
            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyInfo property in properties)
                {
                    row[property.Name] = property.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }

            return table;
        }

        public static DataTable ItemTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("RowID", typeof(int));
            table.Columns.Add("Item", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("UOM", typeof(string));
            table.Columns.Add("Rate", typeof(string));
            table.Columns.Add("Amount", typeof(int));
            table.Columns.Add("RequiredBy", typeof(DateTime));
            return table;
        }

        public static DataTable PRItemTable()
        {
            DataTable ItemTable = new DataTable();
            ItemTable.Columns.Add("RowID", typeof(int));
            ItemTable.Columns.Add("Item", typeof(string));
            ItemTable.Columns.Add("Quantity", typeof(int));
            ItemTable.Columns.Add("UOM", typeof(string));
            ItemTable.Columns.Add("RequiredBy", typeof(DateTime));
            return ItemTable;
        }

        public static DataTable VendorTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("VendorID", typeof(string));
            table.Columns.Add("RowID", typeof(string));
            table.Columns.Add("SendEmail", typeof(bool));
            return table;

        }

        public byte[] GetPdfAsync(string ReportName, string ID)
        {
            byte[] rdlData = con.QuerySingleOrDefault<byte[]>("SELECT RDLData FROM Reports WHERE ReportName = @ReportName", new { ReportName }) ?? throw new Exception("Report not found in the database.");
            using MemoryStream inputStream = new(rdlData);
            ReportWriter writer = new(inputStream);

            if (ReportName == "PurchaseInvoice")
            {
                var prItems = con.Query<PurchaseInvoiceReport>(@"
                    WITH RejectedQuantities AS (
                        SELECT 
                            po.PurchaseOrderID,
                            po.Item,
                            (po.Quantity - gr.AcceptedQuantity) AS RejectedQuantity
                        FROM 
                            [Computer].[dbo].[GoodReceipt] grh
                        INNER JOIN 
                            [Computer].[dbo].[GR_Items] gr ON grh.GoodReceiptID = gr.GoodReceiptID
                        INNER JOIN 
                            [Computer].[dbo].[PO_Items] po ON grh.RefrenceDocument = po.PurchaseOrderID AND gr.Item = po.Item
                        WHERE 
                            grh.RefrenceDocument IS NOT NULL
                    ),
                    InvoiceDetails AS (
                        SELECT 
                            ROW_NUMBER() OVER (ORDER BY i.ItemName) AS Row,
                            pi.PurchaseInvoiceID,
                            pi.AcceptedQuantity,
                            pi.Rate AS Rate,
                            pi.Amount AS Amount,
                            i.ItemName AS Item,
                            v.VendorName AS Vendor,
                            v.Address AS VendorAddress,
		                    v.Contact AS VendorContact,
		                    v.Email AS VendorEmail,
                            u.UOMName AS UOM,
                            FORMAT(p.DueDate, 'yyyy-MM-dd') AS DueDate,
                            p.TotalQuantity AS TotalQuantity,
                            p.TotalAmount AS TotalAmount,
                            FORMAT(p.DocumentDate, 'yyyy-MM-dd') AS DocumentDate,
                            p.RefrenceDocument
                        FROM 
                            PI_Items pi
                        INNER JOIN 
                            PurchaseInvoice p ON pi.PurchaseInvoiceID = p.PurchaseInvoiceID
                        INNER JOIN 
                            Vendor v ON p.VendorID = v.VendorID
                        INNER JOIN 
                            Items i ON pi.Item = i.ItemCode
                        INNER JOIN 
                            UOM u ON pi.UOM = u.UOMID
	                    WHERE
		                    pi.PurchaseInvoiceID = @PurchaseInvoiceID
                    )
                    SELECT 
                        id.Row,
                        id.PurchaseInvoiceID,
                        id.AcceptedQuantity,
	                    COALESCE(rq.RejectedQuantity, 0) AS RejectedQuantity,
                        id.Rate,
                        id.Amount,
                        id.Item,
                        id.Vendor,
                        id.VendorAddress,
	                    id.VendorContact,
	                    id.VendorEmail,
                        id.UOM,
                        id.DueDate,
                        id.TotalQuantity,
                        id.TotalAmount,
                        id.DocumentDate
                    FROM 
                        InvoiceDetails id
                    LEFT JOIN 
                        [Computer].[dbo].[GoodReceipt] grh ON id.RefrenceDocument = grh.GoodReceiptID
                    LEFT JOIN 
                        RejectedQuantities rq ON grh.RefrenceDocument = rq.PurchaseOrderID AND id.Item = rq.Item
                    ORDER BY 
                        id.Row;", new { PurchaseInvoiceID = ID }).ToList();

                writer.DataSources.Add(new ReportDataSource("DataSet1", prItems));
            }

            using MemoryStream memoryStream = new();
            writer.Save(memoryStream, WriterFormat.PDF);

            return memoryStream.ToArray();
        }
    }
}
