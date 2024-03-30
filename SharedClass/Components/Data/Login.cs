using Microsoft.Data.SqlClient;
using Microsoft.JSInterop;

namespace SharedClass.Components.Data
{
    public class Login : Connection
    {
        private SqlCommand? cmd;
        private SqlDataReader? dr;
        protected bool Authorized { get; set; }
        public async Task<bool> Access(string Username, string Password, IJSRuntime JSRuntime)
        {
            try
            {
                if (Username != "" && Password != "")
                {
                    using SqlConnection con = GetSqlConnection();
                    using (cmd = new SqlCommand("SELECT * FROM [Users] WHERE Username = @Username AND Password = @Password", con))
                    {
                        cmd.Parameters.AddWithValue("@Username", Username);
                        cmd.Parameters.AddWithValue("@Password", Password);
                        con.Open();
                        using (dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                Authorized = dr.GetBoolean(dr.GetOrdinal("Authorized"));
                                UpdateAuthorizationStatus(con, Username, true);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", "An error occurred while accessing the database: " + ex.Message.ToString());
                return false;
            }
        }

        private static void UpdateAuthorizationStatus(SqlConnection con, string username, bool isAuthorized)
        {
            con.Close();
            con.Open();
            using (SqlCommand updateCmd = new("UPDATE [Users] SET Authorized = @Authorized WHERE Username = @Username", con))
            {
                updateCmd.Parameters.AddWithValue("@Authorized", isAuthorized);
                updateCmd.Parameters.AddWithValue("@Username", username);
                updateCmd.ExecuteNonQuery();
            }
            con.Close();
        }

        public void LogOut(string AuthUser)
        {
            using SqlConnection conn = GetSqlConnection();
            UpdateAuthorizationStatus(conn, AuthUser, false);
        }
    }
}