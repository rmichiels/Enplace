using Enplace.Service;
using Enplace.Service.Contracts;
using Enplace.Service.Entities;

namespace Enplace
{
    public class EnplaceContext
    {
        public EnplaceContext(ICrudable conman)
        {
            User = conman.Get<User>(1).Result;
        }
        public User? User { get; set; }
    }
}
