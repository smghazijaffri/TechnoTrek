using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.JSInterop;
using Radzen;

namespace SharedClass.Components.Data
{
    public class Login : Connection
    {
        public bool showLoginPopup = false;
        private SqlCommand? cmd;
        private SqlDataReader? dr;

        public void OpenLoginPopup()
        {
            showLoginPopup = true;
        }

        public void CloseLoginPopup()
        {
            showLoginPopup = false;
        }
        public async Task<bool> Access(string Username, string Password, IJSRuntime JSRuntime, NavigationManager navigationManager)
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
                                NavigateToPage(navigationManager);
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

        private void NavigateToPage(NavigationManager navigationManager)
        {
            CloseLoginPopup();
            navigationManager.NavigateTo("/dashboard");
        }
    }
}
