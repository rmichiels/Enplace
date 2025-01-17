﻿using Enplace.Service.DTO;

namespace Enplace.Service.Services.API
{
    public class ClientConfigurationMap
    {
        //private const string _azureHost = "https://localhost:9443";
        private const string _azureHost = "https://enplace.api.swissknife.solutions";
        private const string _idService = "https://id.swissknife.solutions/api/v1";
        //private const string _idService = "http://localhost:8080";

        private readonly Dictionary<string, ClientConfiguration> _configMap = new()
        {
            //{"auth", new("http://localhost:8110", "/api/v1/auth/", false) },
            //{"auth:service", new("https://sk-skid.azurewebsites.net", "/api/v1/auth/", false) },            
            {"auth:service", new(_idService, "/api/v1/auth/", false) },
            {"auth:api", new(_azureHost, "/api/v1/auth/", false) },
            {$"data:{nameof(RecipeDTO)}", new(_azureHost, "/api/v1/Recipes/", true) },
            {$"data:{nameof(IngredientDTO)}", new(_azureHost, "/api/v1/Ingredients/", true) },
            {$"data:{nameof(MenuDTO)}", new(_azureHost, "/api/v1/Menus/", true) },
            {"config", new(_azureHost,"api/v1/Configuration/", true)},
            {"res:ingr", new(_azureHost,"res/q/ingredients", true)},
            {"res:recipe", new(_azureHost,"res/q/recipes", true)}
        };

        public ClientConfiguration this[string label]
        {
            get => _configMap[label];
            set => _configMap[label] = value;
        }

        public bool TryGetValue(string label, out ClientConfiguration? value)
        {
            var result = _configMap.TryGetValue(label, out ClientConfiguration? outValue);
            value = outValue;
            return result;
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
            public string Suffix { get; set; }
            public bool AllowAuthHeader { get; set; }
        }
    }
}
