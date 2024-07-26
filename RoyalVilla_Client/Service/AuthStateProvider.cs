using Blazored.LocalStorage;
using Common;
using Microsoft.AspNetCore.Components.Authorization;
using RoyalVilla_Client.Helper;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace RoyalVilla_Client.Service
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localstorage;


        public AuthStateProvider(HttpClient client, ILocalStorageService localstorage)
        {
            _client = client;
            _localstorage = localstorage; 
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localstorage.GetItemAsync<string>(SD.Local_Token);
            if (token == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token),"jwtAuthType")));
        }
        public void NotifyUserLoggedIn(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }

    }
}
