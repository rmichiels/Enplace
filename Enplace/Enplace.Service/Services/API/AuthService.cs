using Enplace.Service.DTO;
using Enplace.Service.Services.Managers;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Enplace.Service.Services.API
{
    public class AuthService
    {
        private readonly HttpClient _serviceClient;
        private readonly HttpClient _apiClient;
        private readonly AsyncEventManager<Notification> _notifications;
        private int _retryLimit = 3;

        public AuthService(IHttpClientFactory clientFactory, AsyncEventManager<Notification> notificationManager)
        {
            _serviceClient = clientFactory.CreateClient("auth:service");
            _apiClient = clientFactory.CreateClient("auth:api");
            _notifications = notificationManager;
        }

        public async Task<AuthResponse?> RegisterUser(AuthRequest request)
        {
            return await RequestBuilder<AuthResponse>.Create(_serviceClient, "register")
                .ConfigureRequestOptions(new() { RetryLimit = _retryLimit })
                .AddResponseHandler(HttpStatusCode.BadGateway, (_) =>
                {
                    _notifications.TriggerEvent(new() { Type = NotificationType.Warning, Message = "Authentication Service is experiencing a start-up delay, please wait..." });
                    return true;
                })
                .AddResponseHandler(HttpStatusCode.InternalServerError, (_) =>
                {
                    _notifications.TriggerEvent(new() { Message = "An unexpected error has occured.", Type = NotificationType.Error });
                    return false;
                })
                .ExecutePostAsync(request);
        }

        public async Task<AuthResponse?> AuthenticateUser(AuthRequest request)
        {
            return await RequestBuilder<AuthResponse>.Create(_serviceClient, "login")
               .ConfigureRequestOptions(new() { RetryLimit = _retryLimit })
               .AddResponseHandler(HttpStatusCode.BadGateway, (_) =>
               {
                   _notifications.TriggerEvent(new() { Type = NotificationType.Warning, Message = "Authentication Service is experiencing a start-up delay, please wait..." });
                   return true;
               })
                .AddResponseHandler(HttpStatusCode.InternalServerError, (_) =>
                {
                    _notifications.TriggerEvent(new() { Message = "An unexpected error has occured.", Type = NotificationType.Error });
                    return false;
                })
               .ExecutePostAsync(request);
        }

        public async Task<UserDTO> RegisterAPI(string token)
        {
            _apiClient.DefaultRequestHeaders.Authorization = new("Bearer", token);
            var result = await _apiClient.PostAsJsonAsync("register", string.Empty);
            var parse = await JsonSerializer.DeserializeAsync<UserDTO>(await result.Content.ReadAsStreamAsync());
            if (parse is not null)
            {
                return parse;
            }
            else
            {
                throw new Exception("Can't parse User from the Server Response.");
            }
        }

        public async Task<UserDTO> AuthenticateAPI(string token)
        {
            _apiClient.DefaultRequestHeaders.Authorization = new("Bearer", token);
            var result = await _apiClient.PostAsJsonAsync("login", string.Empty);
            var parse = await JsonSerializer.DeserializeAsync<UserDTO>(await result.Content.ReadAsStreamAsync());
            if (parse is not null)
            {
                return parse;
            }
            else
            {
                throw new Exception("Can't parse User from the Server Response.");
            }
        }

        public bool IsTokenValid(string token)
        {
            return true;
        }
    }
}
