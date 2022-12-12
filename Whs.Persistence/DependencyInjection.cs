using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whs.Application.Interfaces;

namespace Whs.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<WhsDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
            services.AddScoped<IWhsDbContext>(provider =>
                provider.GetService<WhsDbContext>());

            return services;
        }
    }
}
