using Enplace.Service.DTO;

namespace Enplace.Library.Contracts
{
    public interface IResource<T>
    {
        public Task<List<T>> Query(string name);
    }

    public interface IResourceProvider<T>
    {
        public Task<Response<T>> Query(params (string Key, string Value)[] queryParameters);
    }
}