using Enplace.Service.Contracts;
using Enplace.Service.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        protected async Task TrackActivityFor(User userToTrack, int itemId)
        {
            var userQuery = await Service.Query<User>();
            var currentUser = await userQuery.Include(u => u.ActivityLog).FirstOrDefaultAsync(u => u.Id == userToTrack.Id);

            if (currentUser is not null)
            {
                var currentTopicLog = currentUser?.ActivityLog.Where(log => log.Topic == typeof(TEntity).Name).OrderBy(log => log.ModifiedOn).ToList();
                if (currentTopicLog?.Count == 10)
                {
                    currentUser?.ActivityLog.Remove(currentTopicLog.Last());
                }
                currentUser?.ActivityLog.Add(new() { User = userToTrack, Topic = typeof(TEntity).Name, ItemID = itemId });
                await Service.Update<User>(currentUser);
            }

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
            var currentUser = await GetUser();
            TEntity entity = await _converter.Convert(DTO);

            var addedEntity = await Service.Add(entity);
            if (addedEntity is null)
            {
                return BadRequest();
            }
            else
            {
                if (currentUser is not null)
                {
                    Task.Run(() => TrackActivityFor(currentUser, entity.Id));
                }
                return Ok(addedEntity);
            }
        }

        [HttpGet]
        [Route("{param}")]
        public async Task<TDTO?> Get(string param)
        {
            var currentUser = await GetUser();

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
                if (currentUser is not null)
                {
                    Task.Run(() => TrackActivityFor(currentUser, entity.Id));
                }
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
            var currentUser = await GetUser();
            var entity = await _converter.Convert(DTO);
            try
            {
                var result = await Service.Update(entity);
                if (currentUser is not null)
                {
                    Task.Run(() => TrackActivityFor(currentUser, entity.Id));
                }
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
