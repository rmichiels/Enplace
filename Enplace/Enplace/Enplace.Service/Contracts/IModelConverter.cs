namespace Enplace.Service.Contracts
{
    public interface IModelConverter<TEntity, TViewModel> where TEntity : class, ILabeled
    {
        public Task<TEntity> Convert(TViewModel viewModel);
        public Task<TViewModel> Convert(TEntity entity);
    }
}