using SharedClass.Components.Model;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Data;
using Dapper;
using System.Reflection;
using SharedClass.Components.Pages.AdminView.Buying;

namespace SharedClass.Components.Data
{
    public class Select : CRUD
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
    }
}
