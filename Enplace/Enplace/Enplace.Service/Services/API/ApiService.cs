using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Services.Managers;
using System.Net;
using System.Net.Http.Json;

namespace Enplace.Service.Services.API
{
    public class ApiService<TEntity> where TEntity : class, ILabeled, new()
    {
        protected readonly HttpClient _client;
        protected readonly AsyncEventManager<Notification> _notificationManager;
        public ApiService(IHttpClientFactory clientFactory, AsyncEventManager<Notification> notificationManager)
        {
            string clientName = $"data:{typeof(TEntity).Name}";
            _client = clientFactory.CreateClient(clientName);
            _notificationManager = notificationManager;
        }
        public async Task<TEntity?> Add(TEntity entity)
        {
            return await RequestBuilder<TEntity>.Create(_client, "add")
                .AddResponseHandler(HttpStatusCode.Unauthorized,
                (m) =>
                {
                    _notificationManager.TriggerEvent(new() { Type = NotificationType.Error, Message = m?.ReasonPhrase ?? string.Empty });
                    return true;
                })
                .ExecutePostAsync(entity);
        }

        public async Task Delete(int id)
        {
            await RequestBuilder<TEntity>.Create(_client, $"{id}")
                .ExecuteDeleteAsync();
        }

        public async Task Delete(string name)
        {
            await RequestBuilder<TEntity>.Create(_client, $"{name}")
                .ExecuteDeleteAsync();
        }

        public async Task<TEntity?> Get(int id)
        {
            return await _client.GetFromJsonAsync<TEntity>($"{id}");
        }
        public async Task<TEntity?> Get(string name)
        {
            return await _client.GetFromJsonAsync<TEntity>($"{name}");
        }

        public async Task<List<TEntity>> GetAll(params (string Key, string Value)[] queryParameters)
        {
            string url = "list";
            string parameters = string.Empty;
            if (queryParameters.Length > 0)
            {
                parameters = string.Join("&", queryParameters.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            }
            if (!string.IsNullOrEmpty(parameters))
            {
                url += "?" + parameters;
            }

            var result = await RequestBuilder<List<TEntity>>.Create(_client, url)
               .AddResponseHandler(HttpStatusCode.Forbidden, (_) =>
               {
                   Console.WriteLine("Not Allowed to Retrieve Entities for User");
                   return false;
               })
               .ExecuteGetAsync() ?? [];

            return result;
        }

        public async Task<TEntity?> Update(TEntity entity)
        {
            return await RequestBuilder<TEntity>.Create(_client)
             .AddResponseHandler(HttpStatusCode.Unauthorized, (m) =>
             {
                 _notificationManager.TriggerEvent(new() { Type = NotificationType.Error, Message = m?.ReasonPhrase ?? string.Empty });
                 return false;
             })
             .ExecutePatchAsync(entity);
        }

        public async Task Like(TEntity entity)
        {
            await RequestBuilder<TEntity>.Create(_client, $"like/{entity.Id}")
           .AddResponseHandler(HttpStatusCode.Unauthorized, (m) =>
           {
               _notificationManager.TriggerEvent(new() { Type = NotificationType.Error, Message = m?.ReasonPhrase ?? string.Empty });
               return false;
           })
             .AddResponseHandler(HttpStatusCode.BadRequest, (m) =>
             {
                 _notificationManager.TriggerEvent(new() { Type = NotificationType.Error, Message = m?.ReasonPhrase ?? string.Empty });
                 return false;
             })
            .AddResponseHandler(HttpStatusCode.NoContent, (m) =>
            {
                _notificationManager.TriggerEvent(new() { Type = NotificationType.Success, Message = $"{entity.Name} Liked!" });
                return false;
            })
           .ExecutePatchAsync(entity);
        }
    }
}
