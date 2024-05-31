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
            return await con.QueryAsync<PurchaseRequisition>("SELECT * FROM PurchaseRequest");
        }

        public async Task<IEnumerable<PurchaseOrders>> GetPODataAsync()
        {
            return await con.QueryAsync<PurchaseOrders>("SELECT * FROM PurchaseOrder");
        }

        public async Task<IEnumerable<RequestForQuotation>> GetRFQDataAsync()
        {

            return await con.QueryAsync<RequestForQuotation>("SELECT * FROM RequestForQuotation");
        }

        public async Task<IEnumerable<GoodReceipt>> GetGRDataAsync()
        {

            return await con.QueryAsync<GoodReceipt>("SELECT * FROM GoodReceipt");
        }

        public async Task<IEnumerable<Quotation>> GetVQDataAsync()
        {
            return await con.QueryAsync<Quotation>("SELECT * FROM Quotation");
        }

        public async Task<IEnumerable<PurchaseInvoice>> GetPIDataAsync()
        {
            return await con.QueryAsync<PurchaseInvoice>("SELECT * FROM PurchaseInvoice");
        }
        public async Task<IEnumerable<Vendor>> GetVendorsDataAsync()
        {
            return await con.QueryAsync<Vendor>("SELECT * FROM Vendor");
        }

        public async Task<IEnumerable<UnitofMeasure>> GetUOMDataAsync()
        {
            return await con.QueryAsync<UnitofMeasure>("SELECT * FROM UOM");
        }

        public async Task<IEnumerable<SalesInvoice>> GetSaleInvoiceDataAsync()
        {
            return await con.QueryAsync<SalesInvoice>("SELECT * FROM SaleInvoice");
        }

        public async Task<IEnumerable<SaleOrder>> GetSaleOrderDataAsync()
        {
            return await con.QueryAsync<SaleOrder>("SELECT * FROM SaleOrder");
        }

        public async Task<IEnumerable<ItemClass>> GetItemsAsync()
        {
            return await con.QueryAsync<ItemClass>("SELECT * FROM Items ORDER BY ItemCode");
        }

        public async Task<IEnumerable<ItemClass>> GetItemsAsync(string ItemType)
        {
            return await con.QueryAsync<ItemClass>("SELECT * FROM Items WHERE ItemType = @ItemType ORDER BY ItemCode", new { ItemType });
        }

        public async Task<IEnumerable<BOM>> GetBOMAsync()
        {
            return await con.QueryAsync<BOM>("SELECT * FROM BOM");
        }

        public async Task<IEnumerable<GoodsIssue>> GetGIAsync()
        {
            return await con.QueryAsync<GoodsIssue>("SELECT * FROM GoodIssue");
        }

        public async Task<IEnumerable<Customer>> GetCustomersDataAsync()
        {
            return await con.QueryAsync<Customer>("SELECT * FROM Customer");
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
    }
}
