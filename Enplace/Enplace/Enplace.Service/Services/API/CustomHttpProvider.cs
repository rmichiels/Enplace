using Blazored.LocalStorage;

namespace Enplace.Service.Services.API
{
    public class CustomHttpProvider : IHttpClientFactory
    {
        private readonly ClientConfigurationMap _map;
        private readonly ISyncLocalStorageService _storageService;

        public CustomHttpProvider(ClientConfigurationMap clientConfigurationMap, ISyncLocalStorageService storageService)
        {
            _map = clientConfigurationMap;
            _storageService = storageService;
        }
        public HttpClient CreateClient(string name)
        {
            if (_map.TryGetValue(name, out var config))
            {
                if (Uri.TryCreate(config?.BaseURL, new UriCreationOptions() { DangerousDisablePathAndQueryCanonicalization = false }, out Uri? uri))
                {
                    HttpClient returnClient = new() { BaseAddress = new Uri(uri, config.Suffix) };
                    if (config.AllowAuthHeader)
                    {
                        string token = EnplaceContext.Token;
                        if (string.IsNullOrEmpty(token))
                        {
                            token = _storageService.GetItemAsString("skid.enplace") ?? string.Empty;
                        }
                        if (!string.IsNullOrEmpty(token))
                        {
                            returnClient.DefaultRequestHeaders.Authorization = new("Bearer", token);
                        }
                    }
                    return returnClient;
                }
                else
                {
                    throw new InvalidOperationException($"{config} can not be parsed as a valid URI for {name}");
                }
            }
            else
            {
                throw new Exception($"{name} is not a registered client.");
            }
        }
    }
}
