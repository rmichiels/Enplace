using Enplace.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Enplace.API
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApiBase<TEntity, TDTO> : ControllerBase where TEntity : class, ILabeled where TDTO : class, ILabeled
    {
        protected ICrudable _service { get; set; }
        protected readonly IModelConverter<TEntity, TDTO> _converter;
        public ApiBase(ICrudable crudService, IModelConverter<TEntity, TDTO> modelConverter)
        {
            _service = crudService;
            _converter = modelConverter;
        }
        [Route("list")]
        [HttpGet]
        public virtual async Task<ICollection<TDTO>> GetAll()
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

        [HttpGet]
        [Route("{param}")]
        public async Task<TDTO?> Get(string param)
        {
            TEntity? entity;
            if (int.TryParse(param, out var id))
            {
                entity = await _service.Get<TEntity>(id);
            }
            else
            {
                entity = await _service.Get<TEntity>(param);
            }

            return await _converter.Convert(entity);
        }
        [Route("{param}")]
        [HttpDelete]
        public async Task<Exception?> Delete(string param)
        {
            if (int.TryParse(param, out var id))
            {
                return await _service.Delete<TEntity>(id);
            }
            else
            {
                return await _service.Delete<TEntity>(param);
            }
        }
        [HttpPatch]
        public virtual async Task<Exception?> Update(TDTO DTO)
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
        [HttpDelete]
        public Task<Exception?> Delete(TDTO entity)
        {
            return _service.Delete(entity);
        }
    }
}
