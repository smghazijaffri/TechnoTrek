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

        public async Task<(bool IsAuthorized, string Role)> Access(string username, string password, IJSRuntime jsRuntime, ISnackbar Snackbar)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return (false, null);
            }

            try
            {
                var query = "SELECT UserName, UserPassword, Role FROM Users WHERE UserName = @Username AND UserPassword = @Password";
                var parameters = new { Username = username, Password = password };
                var userAuth = await con.QueryFirstOrDefaultAsync<UserAuth>(query, parameters);

                if (userAuth != null)
                {
                    await UpdateAuthorizationStatus(username, true);
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