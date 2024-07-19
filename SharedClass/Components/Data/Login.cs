using SharedClass.Components.Model;
using Microsoft.Data.SqlClient;
using MudBlazor;
using Dapper;

namespace SharedClass.Components.Data
{
    public class Login : Connection
    {
        private readonly SqlConnection con;

        public Login()
        {
            con = GetSqlConnection();
        }

        public async Task<bool> Access(string username, string password, ISnackbar Snackbar)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return (false);
            }

            try
            {
                var query = "SELECT TOP(1) UserName, UserPassword FROM Users WHERE UserName = @Username AND Status != 'Disabled'";
                var parameters = new { Username = username };
                var userAuth = await con.QueryFirstOrDefaultAsync<UserAuth>(query, parameters);

                if (userAuth != null && VerifyPassword(password, userAuth.UserPassword))
                {
                    return (true);
                }
                else
                {
                    Snackbar.Clear();
                    Snackbar.Add("Invalid username or password.", Severity.Error);
                    return (false);
                }
            }
            catch (SqlException ex)
            {
                Snackbar.Clear();
                Snackbar.Add($"An error occurred while verifying the credentials: {ex.Message}", Severity.Error);
                return (false);
            }
        }

        public static bool VerifyPassword(string inputPassword, string storedHash)
        {
            string hashedInputPassword = Select.HashPassword(inputPassword);
            return hashedInputPassword == storedHash;
        }
    }
}