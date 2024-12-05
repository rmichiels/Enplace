using Blazored.LocalStorage;

namespace Enplace.Library.Models
{
    public class CachedList<T>
    {
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public int ID { get; set; } = 0;
        public List<T> Items { get; set; } = [];

        public bool IsValid()
        {
            return TimeStamp.AddMinutes(10) > DateTime.Now;
        }
    }

    public class CachedItem<T>
    {
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public int ID { get; set; } = 0;
        public T Item { get; set; }

        public bool IsValid()
        {
            return TimeStamp.AddMinutes(10) > DateTime.Now;
        }
    }

    public static class StorageExtensions
    {
        public static async Task<T?> GetCacheFor<T>(this ILocalStorageService storage, int id, string? keyextension = null)
        {
            var storageKey = $"sk.enplace.cache:{typeof(T).Name}";
            if (keyextension is not null)
            {
                storageKey += "-" + keyextension;
            }
            var result = await storage.GetItemAsync<CachedItem<T>>(storageKey);

            if (result is null || !result.IsValid() || id != result.ID)
            {
                return default;
            }
            else
            {
                return result.Item;
            }
        }

        public static async Task<List<T>> GetCachedListFor<T>(this ILocalStorageService storage, int id, string? keyextension = null)
        {
            var storageKey = $"sk.enplace.cache:{typeof(T).Name}";
            if (keyextension is not null)
            {
                storageKey += "-" + keyextension;
            }
            var result = await storage.GetItemAsync<CachedList<T>>(storageKey);

            if (result is null || !result.IsValid() || id != result.ID)
            {
                return [];
            }
            else
            {
                return result.Items;
            }
        }

        public static async Task SetCacheFor<T>(this ILocalStorageService storage, T itemToCache, int id, string? keyextension = null)
        {
            var storageKey = $"sk.enplace.cache:{typeof(T).Name}";

            if (keyextension is not null)
            {
                storageKey += "-" + keyextension;
            }

            CachedItem<T> cachedList = new()
            {
                ID = id,
                Item = itemToCache
            };
            await storage.SetItemAsync(storageKey, cachedList);
        }

        public static async Task SetCacheFor<T>(this ILocalStorageService storage, List<T> itemsToCache, int id, string? keyextension = null)
        {
            var storageKey = $"sk.enplace.cache:{typeof(T).Name}";

            if (keyextension is not null)
            {
                storageKey += "-" + keyextension;
            }

            CachedList<T> cachedList = new()
            {
                ID = id,
                Items = itemsToCache
            };
            await storage.SetItemAsync(storageKey, cachedList);
        }
    }
}
