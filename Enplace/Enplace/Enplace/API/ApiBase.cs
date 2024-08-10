using Enplace.Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.API
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApiBase<TEntity, TViewModel> : ControllerBase, ICrudable<TEntity> where TEntity : class, ILabeled
    {
        protected ICrudable _service { get; set; }
        private readonly IModelConverter<TEntity, TViewModel> _converter;
        public ApiBase(ICrudable crudService, IModelConverter<TEntity, TViewModel> modelConverter)
        {
            _service = crudService;
            _converter = modelConverter;
        }
        [Route("list")]
        [HttpGet]
        public Task<ICollection<TEntity>> GetAll()
        {
            throw new NotImplementedException();
        }
        [Route("add")]
        [HttpPost]
        public Task<Exception?> Add(TEntity entity)
        {
            throw new NotImplementedException();
        }
        [Route("byid/{id}")]
        [HttpGet]
        public Task<TEntity?> Get(int id)
        {
            throw new NotImplementedException();
        }
        [Route("byid/{id}")]
        [HttpDelete]
        public Task<Exception?> Delete(int id)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [Route("byname/{name}")]
        public Task<TEntity?> Get(string name)
        {
            throw new NotImplementedException();
        }
        [Route("byname/{name}")]
        [HttpDelete]
        public Task<Exception?> Delete(string name)
        {
            throw new NotImplementedException();
        }
        [HttpPatch]
        public Task<Exception?> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
        [HttpPatch]
        [Route("range")]
        public Task<Exception?> MassUpdate(ICollection<TEntity> entities)
        {
            throw new NotImplementedException();
        }
        [HttpDelete]
        public Task<Exception?> Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }
        [HttpDelete]
        [Route("range")]
        public Task<Exception?> MassDelete(ICollection<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
