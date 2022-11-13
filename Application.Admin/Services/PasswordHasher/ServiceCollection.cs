using Microsoft.Extensions.DependencyInjection;

namespace Application.Admin.Services.PasswordHasher
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddPasswordHasher(this IServiceCollection service)
        {
            service.AddTransient<IPasswordHasherService, PasswordHasherService>();
            return service;
        }
    }
}