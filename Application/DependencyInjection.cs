using Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Application.AssembyReference).Assembly);

            services.AddMediatR(options =>
            {
                options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                options.RegisterServicesFromAssemblies(typeof(Application.AssembyReference).Assembly);
            });

            return services;
        }
    }
}
