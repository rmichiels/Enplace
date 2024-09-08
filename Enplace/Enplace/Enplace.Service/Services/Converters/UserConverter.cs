using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Service.Services.Converters
{
    public class UserConverter : IModelConverter<User, UserDTO>
    {
        public async Task<User?> Convert(UserDTO? viewModel)
        {
            if (viewModel == null) return null;
            return new User() { Id = viewModel.Id, Name = viewModel.Name, };
        }

        public async Task<UserDTO?> Convert(User? entity)
        {
            if (entity == null) return null;
            return new UserDTO() { Id = entity.Id, Name = entity.Name, };
        }
    }
}
