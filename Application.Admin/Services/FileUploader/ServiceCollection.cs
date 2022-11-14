using System;
using Application.Admin.Services.FileUploader.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Admin.Services.FileUploader
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddFileUploader(this IServiceCollection service, IConfiguration configuration)
        {
            var fileUploadConfiguration = new FileUploadConfiguration();
            configuration.GetSection(FileUploadConfiguration.Key).Bind(fileUploadConfiguration);

            if (string.IsNullOrWhiteSpace(fileUploadConfiguration.Folder) || string.IsNullOrWhiteSpace(fileUploadConfiguration.HostAddress) )
                throw new Exception("Section 'FileUpload' configuration settings are not found in settings file");
            
            service.AddSingleton(fileUploadConfiguration);
            
            service.AddTransient<IFileUploaderService, FileUploaderService>();
          
            return service;
        }
    }
}