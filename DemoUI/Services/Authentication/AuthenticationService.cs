using Blazored.LocalStorage;
using DemoUI.Providers;
using DemoUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace DemoUI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(LoginUserDto loginModel)
        {
            var response = await httpClient.LoginAsync(loginModel);
            //Store Token
            await localStorage.SetItemAsync("accessToken", response.Token);
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

            return true;
        }


        public async Task<Guid> GetUserIdAsync()
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity.IsAuthenticated)
            {
                var userId = authState.User.FindFirst(c => c.Type == "uid")?.Value;
                return Guid.Parse(userId);
            }
            else
            {
                await Logout();
                return new Guid();
            }
        }

        public async Task<string> GetUserEmailAsync()
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity.IsAuthenticated)
            {
                var email = authState.User.FindFirst(c => c.Type == "email")?.Value;
                return email;
            }
            else
            {
                await Logout();
                return string.Empty;
            }
        }


        public async Task<bool> GetUserIsActive()
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity.IsAuthenticated)
            {
                var active = authState.User.FindFirst(c => c.Type == "active")?.Value;
                return Boolean.Parse(active);
            }
            else
            {
                await Logout();
                return false;
            }
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
        }

    }
}
