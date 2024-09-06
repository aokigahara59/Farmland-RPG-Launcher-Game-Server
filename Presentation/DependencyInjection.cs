using Infrastructure.Jwt;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddControllers();

            JwtSettings settings = new();
            configuration.Bind(JwtSettings.SectionName, settings);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = settings.Issuer,
                        ValidAudience = settings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(settings.Secret))
                    };
                });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(AssemblyReference).Assembly);
            services.AddSingleton(config);

            services.AddMapster();

            return services;
        }
    }
}
