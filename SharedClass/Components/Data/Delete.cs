using Dapper;
using Microsoft.JSInterop;
using Microsoft.Data.SqlClient;

namespace SharedClass.Components.Data
{
    public class Delete : Select
    {
        private readonly SqlConnection con;

        public Delete()
        {
            con = GetSqlConnection();
        }

        public async Task DeleteFromCustomBuilt(string id, string component, IJSRuntime JSRuntime)
        {
            try
            {
                con.Open();

                // Delete the record based on the provided 'id' and 'component'
                string deleteQuery = $"DELETE FROM Custom_Built WHERE Id = @Id";
                await con.ExecuteAsync(deleteQuery, new { Id = id });
                con.Close();
            }
            catch (SqlException ex)
            {
                con.Close();
                await JSRuntime.InvokeVoidAsync("alert", ex.Message.ToString());
            }
        }

        public async Task DeleteFromTable(string table1, string column, string id, string table2)
        {
            try
            {
                con.Close();

                string query1 = $"DELETE FROM {table1} WHERE {column} = @Id";
                string query2 = $"DELETE FROM {table2} WHERE {column} = @Id";

                con.Open();

                await con.ExecuteAsync(query2, new { Id = id });
                await con.ExecuteAsync(query1, new { Id = id });
                
                con.Close();
            }
            catch (SqlException ex)
            {
                con.Close();
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteFromTable(string table1, string column, string id)
        {
            try
            {
                con.Close();

                string query1 = $"DELETE FROM {table1} WHERE {column} = @Id";

                con.Open();

                await con.ExecuteAsync(query1, new { Id = id });

                con.Close();
            }
            catch (SqlException ex)
            {
                con.Close();
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteFromTable(string table1, string column1, string column2, string id, string table2, string table3, string table4)
        {
            try
            {
                con.Close();

                string query1 = $"DELETE FROM {table1} WHERE {column1} = @Id";
                string query2 = $"DELETE FROM {table2} WHERE {column2} = @Id";
                string query3 = $"DELETE FROM {table3} WHERE {column2} = @Id";
                string query4 = $"DELETE FROM {table4} WHERE {column2} = @Id";

                con.Open();

                await con.ExecuteAsync(query4, new { Id = id });
                await con.ExecuteAsync(query3, new { Id = id });
                await con.ExecuteAsync(query2, new { Id = id });
                await con.ExecuteAsync(query1, new { Id = id });

                con.Close();
            }
            catch (SqlException ex)
            {
                con.Close();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
