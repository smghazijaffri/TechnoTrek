using Dapper;
using Microsoft.Data.SqlClient;
using SharedClass.Components.Model;
using System.Data;

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
