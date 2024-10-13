using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Enplace.Service.Services.API
{
    public class RequestBuilder<T> where T : class, new()
    {
        private readonly HttpClient _client;
        private readonly string _endpoint;
        private RequestOptions _options = new();
        private readonly Dictionary<HttpStatusCode, Action<T?, HttpResponseMessage>> _handlers = [];

        protected RequestBuilder(HttpClient client, string endpoint)
        {
            _client = client;
            _endpoint = endpoint;
        }

        public static RequestBuilder<T> Create(HttpClient client, string endpoint = "")
        {
            return new RequestBuilder<T>(client, endpoint);
        }

        public RequestBuilder<T> AddResponseHandler(HttpStatusCode code, Action<T?, HttpResponseMessage> handler)
        {
            _handlers.TryAdd(code, handler);
            return this;
        }

        public RequestBuilder<T> ConfigureRequestOptions(RequestOptions options)
        {
            _options = options;
            return this;
        }

        private async Task<T?> Handle(Task<HttpResponseMessage> request)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            int attempts = 1;
            var result = await request;
            T? parsedResponse = null;
            do
            {
                switch (result.StatusCode)
                {
                    case HttpStatusCode.OK:
                        try
                        {
                            parsedResponse = await JsonSerializer.DeserializeAsync<T>(await result.Content.ReadAsStreamAsync(), options);
                        }
                        catch
                        {
                            return null;
                        }
                        if (parsedResponse is not null)
                        {
                            if (_handlers.TryGetValue(result.StatusCode, out var okResponseHandler))
                            {
                                okResponseHandler.Invoke(parsedResponse, result);
                            }
                            return parsedResponse;
                        }
                        else
                        {
                            return null;
                        }
                    default:
                        if (_handlers.TryGetValue(result.StatusCode, out var action))
                        {
                            action.Invoke(parsedResponse, result);
                        }
                        else
                        {
                            result = await request;
                        }
                        break;
                }
            } while (attempts < _options.RetryLimit);
            return null;
        }

        public async Task<T?> ExecuteGetAsync()
        {
            return await Handle(_client.GetAsync(_endpoint));
        }

        public async Task<T?> ExecutePostAsync(T payload)
        {
            return await Handle(_client.PostAsJsonAsync(_endpoint, payload));
        }

        public async Task<T?> ExecutePatchAsync(T payload)
        {
            return await Handle(_client.PatchAsJsonAsync(_endpoint, payload));
        }

        public async Task ExecuteDeleteAsync()
        {
            await Handle(_client.DeleteAsync(_endpoint));
        }
    }

    public class RequestOptions
    {
        public int RetryLimit { get; set; } = 0;
        public TimeSpan TimeoutLimit { get; set; }
    }
}
