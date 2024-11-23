using Enplace.Service.Contracts;
using Enplace.Service.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Claims;

namespace Enplace.API
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApiBase<TEntity, TDTO> : ControllerBase where TEntity : class, ILabeled where TDTO : class, ILabeled
    {
        protected ICrudable Service { get; set; }
        protected readonly IModelConverter<TEntity, TDTO> _converter;
        public ApiBase(ICrudable crudService, IModelConverter<TEntity, TDTO> modelConverter)
        {
            Service = crudService;
            _converter = modelConverter;
        }

        protected async Task<User?> GetUser()
        {
            var id = User.Identity as ClaimsIdentity;
            var username = id?.FindFirst("username")?.Value ?? string.Empty;
            var user = await Service.Get<User>(username);
            return user;
        }

        [Route("list")]
        [HttpGet]
        public virtual async Task<ICollection<TDTO>> GetAll([FromQuery] bool foruser = false)
        {
            List<TDTO> results = [];
            var intermediary = await Service.GetAll<TEntity>();
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

            var addedEntity = await Service.Add(entity);
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
                entity = await Service.Get<TEntity>(id);
            }
            else
            {
                entity = await Service.Get<TEntity>(param);
            }
            if (entity is null)
            {
                return null;
            }
            else
            {
                return await _converter.Convert(entity);
            }
        }
        [Route("{param}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string param)
        {
            try
            {
                if (int.TryParse(param, out var id))
                {
                    await Service.Delete<TEntity>(id);
                }
                else
                {
                    await Service.Delete<TEntity>(param);
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
                var result = await Service.Update(entity);
                return Ok();
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
                await Service.Delete(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
