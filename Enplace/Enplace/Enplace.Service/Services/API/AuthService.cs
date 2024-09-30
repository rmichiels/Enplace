using Enplace.Service.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Text.Json;

namespace Enplace.Service.Services.API
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private string _token;
        private static AuthenticationState _notAuthenticatedState = new(new System.Security.Claims.ClaimsPrincipal());
        public Dictionary<string, string> Claims { get; set; }
        public Dictionary<string, string> Scopes { get; set; }

        private int _retryLimit = 3;

        public AuthService(IHttpClientFactory clientFactory) { _httpClient = clientFactory.CreateClient("auth"); }

        public async Task<AuthResponse?> RegisterUser(AuthRequest request)
        {
            int attemptedRequests = 0;
            // request loop to deal w/ cold-start timeouts in the dependency chain
            while (attemptedRequests < _retryLimit)
            {
                var result = await _httpClient.PostAsJsonAsync($"register", request);
                if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    return await JsonSerializer.DeserializeAsync<AuthResponse>(await result.Content.ReadAsStreamAsync());
                }
                // if response code falls in the 401...499 range
                // then the response is valid & can be returned
                else if (401 <= (int)result.StatusCode && (int)result.StatusCode < 500)
                {
                    return await JsonSerializer.DeserializeAsync<AuthResponse>(await result.Content.ReadAsStreamAsync());
                }
                else
                {
                    attemptedRequests++;
                }
            }
            return null;
        }

        public async Task<AuthResponse?> AuthenticateUser(AuthRequest request)
        {
            int attemptedRequests = 0;
            // request loop to deal w/ cold-start timeouts in the dependency chain
            while (attemptedRequests < _retryLimit)
            {
                var result = await _httpClient.PostAsJsonAsync($"login", request);
                if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    return await JsonSerializer.DeserializeAsync<AuthResponse>(await result.Content.ReadAsStreamAsync());
                }
                // if response code falls in the 401...499 range
                // then the response is valid & can be returned
                else if (401 <= (int)result.StatusCode && (int)result.StatusCode < 500)
                {
                    return await JsonSerializer.DeserializeAsync<AuthResponse>(await result.Content.ReadAsStreamAsync());
                }
                else
                {
                    attemptedRequests++;
                }
            }
            return null;
        }

        public bool IsTokenValid(string token)
        {
            return true;
        }
    }
}
