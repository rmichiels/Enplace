using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Entities;

namespace Enplace.Service.Services.Converters
{
    public class UserConverter : IModelConverter<User, UserDTO>
    {
        public Task<User> Convert(UserDTO viewModel)
        {
            return Task.FromResult(new User() { Id = viewModel.Id, Name = viewModel.Name });
        }

        public Task<UserDTO> Convert(User entity)
        {
            return Task.FromResult(new UserDTO() { Id = entity.Id, Name = entity.Name });
        }
    }
}
