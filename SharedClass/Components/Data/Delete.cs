using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.JSInterop;

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

    }
}
