using SharedClass.Components.Model;
using Microsoft.Data.SqlClient;
using Microsoft.JSInterop;
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

        public async Task<(bool IsAuthorized, string Role)> Access(string username, string password, ISnackbar Snackbar)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return (false, null);
            }

            try
            {
                var query = "SELECT UserName, UserPassword, Role FROM Users WHERE UserName = @Username AND UserPassword = @Password AND Status != 'Disabled'";
                var parameters = new { Username = username, Password = password };
                var userAuth = await con.QueryFirstOrDefaultAsync<UserAuth>(query, parameters);

                if (userAuth != null)
                {
                    return (true, userAuth.Role);
                }
                else
                {
                    return (false, null);
                }
            }
            catch (SqlException ex)
            {
                Snackbar.Clear();
                Snackbar.Add($"An error occurred while verifying the credentials: {ex.Message}", Severity.Error);
                return (false, null);
            }
        }

        public static bool VerifyPassword(string inputPassword, string storedHash)
        {
            string hashedInputPassword = Select.HashPassword(inputPassword);
            return hashedInputPassword == storedHash;
        }
    }
}