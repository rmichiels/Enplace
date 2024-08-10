using Enplace.Service.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Service
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddEnplaceServices(this IServiceCollection services)
        {
            services.AddDbContext<LiteDBContext>();
            return services;
        }
    }
}
