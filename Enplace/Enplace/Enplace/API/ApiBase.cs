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
        public async Task<IActionResult> Add(TDTO DTO)
        {
            TEntity entity = await _converter.Convert(DTO);

            var addedEntity = await _service.Add(entity);
            if (addedEntity is null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(addedEntity);
            }
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
        public async Task<IActionResult> Delete(string param)
        {
            try
            {
                if (int.TryParse(param, out var id))
                {
                    await _service.Delete<TEntity>(id);
                }
                else
                {
                    await _service.Delete<TEntity>(param);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPatch]
        public virtual async Task<IActionResult> Update(TDTO DTO)
        {
            var entity = await _converter.Convert(DTO);
            try
            {
                var result = await _service.Update(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(TDTO entity)
        {
            try
            {
                await _service.Delete(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
