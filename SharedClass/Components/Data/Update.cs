using Microsoft.Data.SqlClient;
using Microsoft.JSInterop;
using Dapper;

namespace SharedClass.Components.Data
{
    public class Update : Insert
    {
        public async Task UpdateCustomBuilt(string id, string comp, IJSRuntime JSRuntime)
        {
            try
            {
                using (SqlConnection con = GetSqlConnection())
                {
                    con.Open();
                    // Record exists, update the Name and Contact
                    string updateQuery = $"UPDATE Custom_Built SET {comp} = {null} WHERE Id = @Id";
                    await con.ExecuteAsync(updateQuery, new { Id = id });
                }
            }
            catch (SqlException ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", ex.Message.ToString());
            }
        }
    }
}
