using Blazored.LocalStorage;
using Enplace.Service;
using Enplace.Service.Services.API;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Enplace.WASM.Services
{
    public class SwissknifeAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _storage;
        private readonly AuthService _auth;
        public SwissknifeAuthStateProvider(ILocalStorageService storage, AuthService auth)
        {
            _storage = storage;
            _auth = auth;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = EnplaceContext.Token ?? await _storage.GetItemAsStringAsync("skid.enplace") ?? string.Empty;
            var user = new ClaimsPrincipal();
            bool tokenIsValid = false;

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var validations = new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidAudience = "Enplace",
                    ValidIssuer = "sk.id",
                    ValidateIssuerSigningKey = false,
                    SignatureValidator = (token, _) => new JwtSecurityToken(token)
                };

                try
                {
                    user = handler.ValidateToken(token, validations, out var _);
                    tokenIsValid = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                // Assign token to  context & re-fetch user data
                // if token retrieved from storage is valid
                if (string.IsNullOrEmpty(EnplaceContext.Token) && tokenIsValid)
                {
                    EnplaceContext.Token = token;
                    EnplaceContext.User = await _auth.AuthenticateAPI(token);
                }
            }

            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }
    }
}
