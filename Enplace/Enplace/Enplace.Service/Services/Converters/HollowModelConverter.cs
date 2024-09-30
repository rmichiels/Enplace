using Enplace.Service.Contracts;

namespace Enplace.Service.Services.Converters
{
    public class HollowModelConverter<TEntity> : IModelConverter<TEntity, TEntity> where TEntity : class, ILabeled
    {
        public async Task<TEntity> Convert(TEntity viewModel)
        {
            return viewModel;
        }
    }
}
