using Blazored.LocalStorage;
using Enplace.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Service.Services.Managers.Cache
{
    public class LocalCacheManager : ICacheManager
    {
        private readonly ILocalStorageService _storage;
        private double _minutesAllowedLifetime = 10;

        public LocalCacheManager(ILocalStorageService storage)
        {
            _storage = storage;
        }

        public async Task<TCachedItem> GetItem<TCachedItem>(string key, Task<TCachedItem> onCacheInvalid)
        {
            var result = await _storage.GetItemAsync<CachedItem<TCachedItem>>($"{nameof(CachedItem<TCachedItem>)}:{key}");
            if (result is not null && result.IsValid(_minutesAllowedLifetime))
            {
                return result.Item;
            }
            else
            {
                var freshItem = await onCacheInvalid;
                if (freshItem is not null)
                {
                    var ItemCache = new CachedItem<TCachedItem>() { Item = freshItem };
                    await _storage.SetItemAsync($"{nameof(CachedItem<TCachedItem>)}:{key}", ItemCache);
                }
                return freshItem;
            }
        }

    }
}
