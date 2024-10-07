using Enplace.Service.DTO;
using Enplace.Service.Entities;

namespace Enplace.Service.Services.API
{
    public class ClientConfigurationMap
    {
        private readonly Dictionary<string, ClientConfiguration> _configMap = new()
        {
            //{"auth", new("http://localhost:8110", "/api/v1/auth/", false) },
            {"auth:service", new("https://sk-skid.azurewebsites.net", "/api/v1/auth/", false) },
            {"auth:api", new("https://localhost:7283", "/api/v1/auth/", false) },
            {$"data:{nameof(RecipeDTO)}", new("https://localhost:7283", "/api/v1/Recipes/", true) },
            {$"data:{nameof(MenuDTO)}", new("https://localhost:7283", "/api/v1/Menus/", true) },
            {"config", new("https://localhost:7283","api/v1/Configuration/", true)},
            {"res:ingr", new("https://localhost:7283","res/q/ingredients", true)}
        };

        public ClientConfiguration this[string label]
        {
            get => _configMap[label];
            set => _configMap[label] = value;
        }

        public bool TryGetValue(string label, out ClientConfiguration? value)
        {
            if (_configMap.ContainsKey(label))
            {
                value = _configMap[label];
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        public class ClientConfiguration
        {
            public ClientConfiguration(string baseURL, string suffix, bool allowAuthHeader)
            {
                BaseURL = baseURL;
                Suffix = suffix;
                AllowAuthHeader = allowAuthHeader;
            }

            public string BaseURL { get; set; }
            public string Suffix { get; set; } = string.Empty;
            public bool AllowAuthHeader { get; set; } = true;
        }
    }
}
