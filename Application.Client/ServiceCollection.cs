using System;
using System.Reflection;
using Application.Client.Common.Behaviours;
using Application.Client.Common.Configurations;
using Application.Client.Common.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Client
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            //services.AddTransient<IPermissionService, PermissionService>();

            
            var fileUploadConfiguration = new FileUploadConfiguration();
            configuration.GetSection(FileUploadConfiguration.Key).Bind(fileUploadConfiguration);

            if (string.IsNullOrWhiteSpace(fileUploadConfiguration.Folder) || string.IsNullOrWhiteSpace(fileUploadConfiguration.HostAddress) )
                throw new Exception("Section 'FileUpload' configuration settings are not found in settings file");
            
            services.AddSingleton(fileUploadConfiguration);
            
            services.AddSieveProcessors();
            return services;
        }
    }
}