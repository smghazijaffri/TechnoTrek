using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.JSInterop;

namespace SharedClass.Components.Data
{
    public class Update : Insert
    {
        string? Iid;
        private readonly SqlConnection con;
        public Update()
        {
            con = GetSqlConnection();
        }

        public void Uupdate(string id)
        {
            Iid = id;
        }
        public async Task UpdateCustomBuilt(string comp, IJSRuntime JSRuntime)
        {
            try
            {
                con.Open();
                // Record exists, update the Name and Contact
                string updateQuery = $"UPDATE Custom_Built SET [{comp}] = null WHERE Id = @Id";
                await con.ExecuteAsync(updateQuery, new { Id = Iid });
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
