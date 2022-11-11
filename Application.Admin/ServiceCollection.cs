using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Admin
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services,
            IConfiguration configuration)
        {
            /*services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient<IPermissionService, PermissionService>();*/
            return services;
        }
    }
}