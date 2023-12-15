using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace SharedClass.Components.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _user;
        private readonly NavigationManager _navigationManager;

        public CustomAuthenticationStateProvider(NavigationManager navigationManager)
        {
            _user = new ClaimsPrincipal(new ClaimsIdentity());
            _navigationManager = navigationManager;
        }

        public void MarkUserAsAuthenticated(string username)
        {
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, username),
        }, "custom");

            _user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void MarkUserAsLoggedOut()
        {
            _user = new ClaimsPrincipal(new ClaimsIdentity());

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void NavigateToLogin()
        {
            _navigationManager.NavigateTo("/");
        }

        public void Logout()
        {
            MarkUserAsLoggedOut();
            NavigateToLogin();
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_user));
        }
    }
}
