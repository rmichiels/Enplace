using Enplace.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Enplace.API
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApiBase<TEntity, TDTO> : ControllerBase, ICrudable<TDTO> where TEntity : class, ILabeled where TDTO : class, ILabeled
    {
        protected ICrudable _service { get; set; }
        private readonly IModelConverter<TEntity, TDTO> _converter;
        public ApiBase(ICrudable crudService, IModelConverter<TEntity, TDTO> modelConverter)
        {
            _service = crudService;
            _converter = modelConverter;
        }
        [Route("list")]
        [HttpGet]
        public async Task<ICollection<TDTO>> GetAll()
        {
            List<TDTO> results = [];
            var intermediary = await _service.GetAll<TEntity>();
            foreach (TEntity item in intermediary)
            {
                var DTO = await _converter.Convert(item);
                if (DTO is not null)
                {
                    results.Add(DTO);
                }
            }
            return results;
        }
        [Route("add")]
        [HttpPost]
        public async Task<Exception?> Add(TDTO DTO)
        {
            TEntity? entity = await _converter.Convert(DTO);
            if (entity is not null)
            {
                return await _service.Add(entity);
            }
            else
            {
                return new Exception("Entity Conversion Failed & Resulted in 'null'");
            };
        }

        [Route("byid/{id}")]
        [HttpGet]
        public async Task<TDTO?> Get(int id)
        {
            var entity = await _service.Get<TEntity>(id);
            return await _converter.Convert(entity);
        }
        [Route("byid/{id}")]
        [HttpDelete]
        public Task<Exception?> Delete(int id)
        {
            return _service.Delete<TEntity>(id);
        }
        [HttpGet]
        [Route("byname/{name}")]
        public async Task<TDTO?> Get(string name)
        {
            var entity = await _service.Get<TEntity>(name);
            return await _converter.Convert(entity);
        }
        [Route("byname/{name}")]
        [HttpDelete]
        public Task<Exception?> Delete(string name)
        {
            return _service.Delete<TEntity>(name);
        }
        [HttpPatch]
        public async Task<Exception?> Update(TDTO DTO)
        {
            var entity = await _converter.Convert(DTO);
            if (entity is not null)
            {
                return await _service.Update(entity);
            }
            else
            {
                return new Exception("Entity Conversion Failed & Resulted in 'null'");
            }
        }
        [HttpPatch]
        [Route("range")]
        public Task<Exception?> MassUpdate(ICollection<TDTO> entities)
        {
            return _service.MassUpdate(entities);
        }
        [HttpDelete]
        public Task<Exception?> Delete(TDTO entity)
        {
            return _service.Delete(entity);
        }
        [HttpDelete]
        [Route("range")]
        public Task<Exception?> MassDelete(ICollection<TDTO> entities)
        {
            return _service.MassDelete(entities);
        }
    }
}
