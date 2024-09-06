using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Data;

namespace Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(configuration["ConnectionString"], new MySqlServerVersion(ServerVersion.Parse("9.0")));
            });
            services.AddScoped<IPlayerService, PlayerService>();

            return services;
        }
    }
}
