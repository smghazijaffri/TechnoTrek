using Dapper;
using Microsoft.Data.SqlClient;
using SharedClass.Components.Model;

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

        public async Task<IEnumerable<ItemClass>> GetItemsAsync(string ItemType)
        {
            con.Close();
            con.Open();
            return await con.QueryAsync<ItemClass>("SELECT * FROM Items WHERE ItemType = @ItemType ORDER BY ItemCode", new { ItemType });
        }

        //public async Task<string> GetItemNameAsync(string ItemCode)
        //{
        //    con.Close();
        //    con.Open();
        //    return await con.QuerySingleOrDefaultAsync<string>("SELECT ItemName FROM Items WHERE ItemCode = @ItemCode ORDER BY ItemCode", new { ItemCode });
        //}

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
    }
}