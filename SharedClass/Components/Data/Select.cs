using Dapper;
using Microsoft.Data.SqlClient;
using SharedClass.Components.Model;
using System.Data;
using System.Xml.Linq;

namespace SharedClass.Components.Data
{
    public class Select : Update
    {
        private readonly SqlConnection con;

        public Select()
        {
            con = GetSqlConnection();
        }

        public async Task<IEnumerable<PurchaseRequisition>> GetPR1DataAsync()
        {
            con.Close();
            con.Open();
            return await con.QueryAsync<PurchaseRequisition>("SELECT * FROM PurchaseRequest");
        }

        public async Task<IEnumerable<PurchaseOrders>> GetPODataAsync()
        {
            con.Close();
            con.Open();
            return await con.QueryAsync<PurchaseOrders>("SELECT * FROM PurchaseOrder");
        }

        public List<PurchaseRequisition> PurhcaseRequest(string PRNumber)
        {
            con.Close(); con.Open();
            return con.Query<PurchaseRequisition>("select * from PurchaseRequest where PRnumber = @PRnumber", new { PRnumber = PRNumber }).ToList();
        }

        public async Task<IEnumerable<RequestForQuotation>> GetRFQDataAsync()
        {
            con.Close();
            con.Open();
            return await con.QueryAsync<RequestForQuotation>("SELECT * FROM RequestForQuotation");
        }

        public async Task<IEnumerable<GoodReceipt>> GetGRDataAsync()
        {
            con.Close();
            con.Open();
            return await con.QueryAsync<GoodReceipt>("SELECT * FROM GoodReceipt");
        }

        public async Task<IEnumerable<Quotation>> GetVQDataAsync()
        {
            con.Close();
            con.Open();
            return await con.QueryAsync<Quotation>("SELECT * FROM Quotation");
        }

        public async Task<IEnumerable<PurchaseInvoice>> GetPIDataAsync()
        {
            con.Close();
            con.Open();
            return await con.QueryAsync<PurchaseInvoice>("SELECT * FROM PurchaseInvoice");
        }
        public async Task<IEnumerable<Vendor>> GetVendorsDataAsync()
        {
            con.Close();
            con.Open();
            return await con.QueryAsync<Vendor>("SELECT * FROM Vendor");
        }

        public async Task<IEnumerable<UnitofMeasure>> GetUOMDataAsync()
        {
            con.Close();
            con.Open();
            return await con.QueryAsync<UnitofMeasure>("SELECT * FROM UOM");
        }

        public async Task<IEnumerable<SalesInvoice>> GetSaleInvoiceDataAsync()
        {
            con.Close();
            con.Open();
            return await con.QueryAsync<SalesInvoice>("SELECT * FROM SaleInvoice");
        }

        public async Task<IEnumerable<SaleOrder>> GetSaleOrderDataAsync()
        {
            con.Close();
            con.Open();
            return await con.QueryAsync<SaleOrder>("SELECT * FROM SaleOrder");
        }

        public async Task<IEnumerable<ItemClass>> GetItemsAsync()
        {
            con.Close();
            con.Open();
            return await con.QueryAsync<ItemClass>("SELECT * FROM Items ORDER BY ItemCode");
        }

        //public async Task<IEnumerable<ItemClass>> GetItemsAsync(string ItemType)
        //{
        //    con.Close();
        //    con.Open();
        //    return await con.QueryAsync<ItemClass>("SELECT * FROM Items WHERE ItemType = @ItemType ORDER BY ItemCode", new { ItemType });
        //}

        public async Task<IEnumerable<ItemClass>> GetItemsAsync(string ItemType, List<string> compatibleItemCodes = null)
        {
            con.Close();
            con.Open();

            var query = "SELECT * FROM Items WHERE ItemType = @ItemType";

            if (compatibleItemCodes != null)
            {
                query += " AND ItemCode IN @CompatibleItemCodes";
            }

            query += " ORDER BY ItemCode";

            return await con.QueryAsync<ItemClass>(query, new { ItemType, CompatibleItemCodes = compatibleItemCodes });
        }

        public async Task<IEnumerable<string>> GetCompatibleItemCodesAsync(string itemCode)
        {
            con.Close();
            con.Open();
            var query = "SELECT CompatibilityID FROM ItemCompability WHERE ItemID = @ItemID";
            return await con.QueryAsync<string>(query, new { ItemID = itemCode });
        }

        public async Task<IEnumerable<BOM>> GetBOMAsync()
        {
            con.Close();
            con.Open();
            return await con.QueryAsync<BOM>("SELECT * FROM BOM");
        }

        public async Task<IEnumerable<GoodsIssue>> GetGIAsync()
        {
            con.Close();
            con.Open();
            return await con.QueryAsync<GoodsIssue>("SELECT * FROM GoodIssue");
        }
        public DataTable ConvertListToDataTable(List<string> list)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Value", typeof(string));

            foreach (string s in list)
            {
                table.Rows.Add(s);
            }

            return table;
        }
    }
}
