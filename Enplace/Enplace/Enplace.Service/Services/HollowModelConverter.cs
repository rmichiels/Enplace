using Enplace.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Service.Services
{
    public class HollowModelConverter<TEntity> : IModelConverter<TEntity, TEntity> where TEntity : class, ILabeled
    {
        public async Task<TEntity> Convert(TEntity viewModel)
        {
            return viewModel;
        }
    }
}
