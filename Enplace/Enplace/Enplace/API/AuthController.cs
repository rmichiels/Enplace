using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Entities;
using Enplace.Service.Services.Converters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace Enplace.API
{
    [Authorize]
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        private readonly ICrudable _db;
        public AuthController(ICrudable db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("register")]
        [SwaggerResponse(202, "Accepts the User Registration, and returns the Client Model for the User.", typeof(UserDTO))]
        [SwaggerResponse(400, "Rejects the User Registration, either due to the claims of the JWT being incorrect or missing.")]
        [SwaggerResponse(422, "Failure to process the User Registration, the user might already exist.")]
        public async Task<IActionResult?> Register()
        {
            var id = User.Identity as ClaimsIdentity;
            if (id is not null)
            {
                User user = new()
                {
                    Name = id.FindFirst("username")?.Value ?? string.Empty,
                    SKID = Guid.Parse(id.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty)
                };

                var response = await _db.Add(user);
                if (response is not null)
                {
                    {
                        UserConverter converter = new();
                        return Accepted(await converter.Convert(response));
                    }
                }
                else
                {
                    return UnprocessableEntity();
                }
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("login")]
        [SwaggerResponse(202, "Accepts the User Login, and returns the Client Model for the User.", typeof(UserDTO))]
        [SwaggerResponse(400, "Login Request is invalid, due to either missing or incorrect Claims.")]
        [SwaggerResponse(403, "Login Request is valid, but the request fails validation.")]
        [SwaggerResponse(404, "Login Request is valid, but User is not present in the system.")]

        public async Task<IActionResult?> Login()
        {
            var id = User.Identity as ClaimsIdentity;
            if (id is not null)
            {
                var response = await _db.Get<User>(id.FindFirst("username")?.Value ?? string.Empty);
                if (response is null)
                {
                    User user = new()
                    {
                        Name = id.FindFirst("username")?.Value ?? string.Empty,
                        SKID = Guid.Parse(id.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty)
                    };

                    var result = await _db.Add(user);
                    if (result is not null)
                    {
                        {
                            UserConverter converter = new();
                            return Accepted(await converter.Convert(result));
                        }
                    }
                    else
                    {
                        return UnprocessableEntity();
                    }
                }
                else
                {
                    Guid skidClaim = Guid.Parse(id.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
                    if (response.SKID == skidClaim && skidClaim != Guid.Empty)
                    {
                        var converter = new UserConverter();
                        return Accepted(await converter.Convert(response));
                    }
                    else
                    {
                        return Forbid();
                    }
                }
            }
            return BadRequest();
        }
    }
}
