using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.JSInterop;

namespace SharedClass.Components.Data
{
    public class Insert : Connection
    {
        protected string? Customer_Name { get; set; }
        protected string? Customer_Contact { get; set; }
        protected string? Customer_Id { get; set; }
        private readonly SqlConnection con;
        public Insert()
        {
            con = GetSqlConnection();
        }
        public async Task InsertIntoCustomBuilt(string id, string name, string contact, IJSRuntime JSRuntime)
        {
            try
            {
                Customer_Name = name;
                Customer_Contact = contact;
                Customer_Id = id;
                string formattedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                con.Open();
                string insertQuery = "INSERT INTO Custom_Built (Id, Name, Contact, Date) VALUES (@Id, @Name, @Contact, @Date)";
                await con.ExecuteAsync(insertQuery, new { Id = id, Name = name, Contact = contact, Date = formattedDate });
                con.Close();
            }
            catch (SqlException ex)
            {
                con.Close();
                await JSRuntime.InvokeVoidAsync("alert", ex.Message.ToString());
            }
        }

        public async Task InsertIntoCustomBuilt(string combinedValue, string component, IJSRuntime JSRuntime)
        {
            try
            {
                con.Open();

                // First, check if a record with the given 'name' exists
                string checkQuery = "SELECT COUNT(*) FROM Custom_Built WHERE (Name = @Name AND Contact = @Contact AND Id = @Id)";
                int count = await con.ExecuteScalarAsync<int>(checkQuery, new { Name = Customer_Name, Contact = Customer_Contact, Id = Customer_Id });

                if (count > 0)
                {
                    // If a record with the given 'name' exists, update it
                    string updateQuery = $"UPDATE Custom_Built SET {component} = @Item WHERE (Name = @Name AND Contact = @Contact AND Id = @Id)";
                    await con.ExecuteAsync(updateQuery, new { Item = combinedValue, Name = Customer_Name, Contact = Customer_Contact, Id = Customer_Id });
                    con.Close();
                }
                else
                {
                    // If a record with the given 'name' doesn't exist, insert a new record
                    string insertQuery = $"INSERT INTO Custom_Built ({component}) VALUES (@Item)";
                    await con.ExecuteAsync(insertQuery, new { Item = combinedValue, Name = Customer_Name, Contact = Customer_Contact, Id = Customer_Id });
                    con.Close();
                }
            }
            catch (SqlException ex)
            {
                con.Close();
                await JSRuntime.InvokeVoidAsync("alert", ex.Message.ToString());
            }
        }
    }
}
