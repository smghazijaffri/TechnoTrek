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

        public async Task<bool> Access(string username, string password, IJSRuntime jsRuntime, ISnackbar Snackbar)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            try
            {
                var query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
                var parameters = new { Username = username, Password = password };
                var Authorized = await con.QueryFirstOrDefaultAsync<bool>(query, parameters);

                if (Authorized)
                {
                    await UpdateAuthorizationStatus(username, true);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Snackbar.Clear();
                Snackbar.Add($"An error occurred while verifying the credentials: {ex.Message}", Severity.Error);
                return false;
            }
        }

        private async Task UpdateAuthorizationStatus(string username, bool isAuthorized)
        {
            var updateQuery = "UPDATE Users SET Authorized = @Authorized WHERE Username = @Username";
            var updateParameters = new { Authorized = isAuthorized, Username = username };
            await con.ExecuteAsync(updateQuery, updateParameters);
        }

        public async Task LogOut(string authUser)
        {
            await UpdateAuthorizationStatus(authUser, false);
        }
    }
}