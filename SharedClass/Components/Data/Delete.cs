using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedClass.Components.Data
{
    public class Delete : Insert
    {
        public async Task DeleteFromCustomBuilt(string id, string component, IJSRuntime JSRuntime)
        {
            try
            {
                using (SqlConnection con = GetSqlConnection())
                {
                    con.Open();

                    // Delete the record based on the provided 'id' and 'component'
                    string deleteQuery = $"DELETE FROM Custom_Built WHERE Id = @Id";
                    await con.ExecuteAsync(deleteQuery, new { Id = id });
                }
            }
            catch (SqlException ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", ex.Message.ToString());
            }
        }

    }
}
