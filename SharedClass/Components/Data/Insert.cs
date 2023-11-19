using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.JSInterop;

namespace SharedClass.Components.Data
{
    public class Insert : Connection
    {
        protected string Customer_Name { get; set; }
        protected string Customer_Contact { get; set; }
        protected string Customer_Id { get; set; }
        public async Task InsertIntoCustomBuilt(string id, string name, string contact, IJSRuntime JSRuntime)
        {
            try
            {
                using (SqlConnection con = GetSqlConnection())
                {
                    Customer_Name = name;
                    Customer_Contact = contact;
                    Customer_Id = id;
                    con.Open();

                    // Check if the record already exists
                    string checkQuery = "SELECT COUNT(*) FROM Custom_Built WHERE Id = @Id";
                    int existingRecordsCount = await con.ExecuteScalarAsync<int>(checkQuery, new { Id = id });

                    string formattedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    if (existingRecordsCount > 0)
                    {
                        // Record exists, update the Name and Contact
                        string updateQuery = "UPDATE Custom_Built SET Name = @Name, Contact = @Contact, Date = @Date WHERE Id = @Id";
                        await con.ExecuteAsync(updateQuery, new { Id = id, Name = name, Contact = contact, Date = formattedDate });
                    }
                    else
                    {
                        // Record does not exist, insert the data
                        string insertQuery = "INSERT INTO Custom_Built (Id, Name, Contact, Date) VALUES (@Id, @Name, @Contact, @Date)";
                        await con.ExecuteAsync(insertQuery, new { Id = id, Name = name, Contact = contact, Date = formattedDate });
                    }
                }
            }
            catch (SqlException ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", ex.Message.ToString());
            }
        }

        public async Task InsertIntoCustomBuilt(string combinedValue, string component, IJSRuntime JSRuntime)
        {
            try
            {
                using (SqlConnection con = GetSqlConnection())
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
                    }
                    else
                    {
                        // If a record with the given 'name' doesn't exist, insert a new record
                        string insertQuery = $"INSERT INTO Custom_Built ({component}) VALUES (@Item)";
                        await con.ExecuteAsync(insertQuery, new { Item = combinedValue, Name = Customer_Name, Contact = Customer_Contact, Id = Customer_Id });
                    }
                }
            }
            catch (SqlException ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", ex.Message.ToString());
            }
        }

        public async Task PRInsert(int prnum, string vendor, string item, string quantity, string uom, DateTime creationdate, DateTime reqdat, IJSRuntime JSRuntime)
        {
            try
            {
                using (SqlConnection con = GetSqlConnection())
                {
                    con.Open();

                    string formattedDate = reqdat.ToString("yyyy-MM-dd");
                    string format = creationdate.ToString("yyyy-MM-dd");

                    string sqlQuery = $"INSERT INTO PR1 (PRnumber, Vendor, Item, Quantity, UOM, CreationDate, Reqdate) VALUES (@PRNum, @Vend, @Item, @Quantity, @UOM, @Creation, @Date)";
                    await con.ExecuteAsync(sqlQuery, new { PRNum = prnum, Vend = vendor, Item = item, Quantity = quantity, UOM = uom, Creation = format, Date = reqdat });
                }
            }
            catch (SqlException ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", ex.Message.ToString());
            }
        }
    }
}
