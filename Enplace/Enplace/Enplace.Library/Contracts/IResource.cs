namespace Enplace.Library.Contracts
{
    public interface IResource<T>
    {
        public Task<List<T>> Query(string name);
    }
}