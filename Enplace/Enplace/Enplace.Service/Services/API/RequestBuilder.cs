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
        private readonly Dictionary<HttpStatusCode, Func<HttpResponseMessage, bool>> _handlers = [];

        protected RequestBuilder(HttpClient client, string endpoint)
        {
            _client = client;
            _endpoint = endpoint;
        }

        public static RequestBuilder<T> Create(HttpClient client, string endpoint = "")
        {
            return new RequestBuilder<T>(client, endpoint);
        }

        public RequestBuilder<T> AddResponseHandler(HttpStatusCode code, Func<HttpResponseMessage, bool> handler)
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

            bool allowRetries = true;
            int attempts = 0;

            T? parsedResponse = null;
            while (attempts <= _options.RetryLimit && allowRetries)
            {
                var result = await request;
                attempts++;
                switch (result.StatusCode)
                {
                    case HttpStatusCode.Accepted:
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
                                okResponseHandler.Invoke(result);
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
                            allowRetries = action.Invoke(result);
                        }
                        break;
                }
            }
            return null;
        }

        public async Task<T?> ExecuteGetAsync()
        {
            return await Handle(_client.GetAsync(_endpoint));
        }

        public async Task<T?> ExecutePostAsync(object payload)
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
