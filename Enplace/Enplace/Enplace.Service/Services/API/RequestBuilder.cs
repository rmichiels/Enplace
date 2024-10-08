using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Enplace.Service.Services.API
{
    public class RequestBuilder<T> where T : class, new()
    {
        private readonly HttpClient _client;
        private readonly string _endpoint;
        private RequestOptions _options = new();
        private Dictionary<HttpStatusCode, Action<T, HttpResponseMessage>> _handlers = [];

        protected RequestBuilder(HttpClient client, string endpoint)
        {
            _client = client;
            _endpoint = endpoint;
        }

        public static RequestBuilder<T> Create(HttpClient client, string endpoint)
        {
            return new RequestBuilder<T>(client, endpoint);
        }

        public RequestBuilder<T> AddResponseHandler(HttpStatusCode code, Action<T, HttpResponseMessage> handler)
        {
            _handlers.TryAdd(code, handler);
            return this;
        }

        public RequestBuilder<T> ConfigureRequestOptions(RequestOptions options)
        {
            _options = options;
            return this;
        }

        public async Task<(HttpStatusCode, T)> ExecuteGetAsync()
        {
            int attempts = 1;
            var result = await _client.GetAsync(_endpoint);
            T? parsedResponse = null;
            do
            {
                switch (result.StatusCode)
                {
                    case HttpStatusCode.OK:
                        try
                        {
                            parsedResponse = await JsonSerializer.DeserializeAsync<T>(await result.Content.ReadAsStreamAsync());
                        }
                        catch
                        {
                            return (HttpStatusCode.InternalServerError, new T());
                        }
                        if (parsedResponse is not null)
                        {
                            return (HttpStatusCode.OK, parsedResponse);
                        }
                        else
                        {
                            return (HttpStatusCode.InternalServerError, new T());
                        }
                    default:
                        if (_handlers.TryGetValue(result.StatusCode, out var action))
                        {
                            action.Invoke(parsedResponse, result);
                        }
                        else
                        {
                            result = await _client.GetAsync(_endpoint);
                        }
                        break;
                }
            } while (attempts < _options.RetryLimit);
            return (result.StatusCode, new T());
        }
    }

    public class RequestOptions
    {
        public int RetryLimit { get; set; } = 0;
        public TimeSpan TimeoutLimit { get; set; }
    }
}
