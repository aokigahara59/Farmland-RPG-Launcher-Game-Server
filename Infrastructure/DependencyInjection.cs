using Application.Common.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IUuidGenerator, UuidGenerator>();
            services.AddScoped<IAccessTokenGenerator, AccessTokenGenerator>();

            services.AddSingleton<ISkinService>(provider =>
            {
                var skinDirectory = Path.Combine(configuration.GetValue<string>(WebHostDefaults.ContentRootKey), "wwwroot", "skins");

                if (!Directory.Exists(skinDirectory))
                    Directory.CreateDirectory(skinDirectory);

                var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
                return new SkinService(skinDirectory, "link/to/skin", httpContextAccessor);
            });

            return services;
        }
    }
}
