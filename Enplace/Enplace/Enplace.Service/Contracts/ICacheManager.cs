namespace Enplace.Service.Contracts
{
    public interface ICacheManager
    {
        Task<TCachedItem> GetItem<TCachedItem>(string key, Task<TCachedItem> onCacheInvalid);
    }
}