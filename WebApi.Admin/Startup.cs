using System;
using Application.Admin;
using Application.Admin.Common.Extensions;
using Application.Admin.Common.Interfaces;
using Application.Admin.Features.Authors.Commands.CreateAuthor;
using Application.Admin.Services.PasswordHasher;
using FluentValidation.AspNetCore;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApi.Admin.Common.Extensions;
using WebApi.Admin.Common.Filters;
using WebApi.Admin.Common.Services;

namespace WebApi.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(config => { config.Filters.Add<ErrorValidationHandlerActionFilter>(); })
                .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; })
                .AddJsonOptions(options => { options.JsonSerializerOptions.IgnoreNullValues = true; })
                .AddMvcOptions(o => o.AllowEmptyInputInBodyModelBinding = false)
                //.AddMvcOptions(o => o.Filters.Add(typeof(ShowWithPermissionHandlerFilter)))
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateAuthorCommand>());
            
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

            services.AddApplicationLayer(Configuration);
            services.AddApplicationAuthentication(Configuration);
            services.AddAdminPersistenceInfrastructureLayer(Configuration);
            services.AddPasswordHasher();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AXL Book Catalog Admin API",
                    Version = "v1",
                    Description = "Description for the API goes here.",
                    Contact = new OpenApiContact
                    {
                        Name = "Karomatullo Hayoev",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/hayoev"),
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseErrorHandler();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint($"/swagger/v1/swagger.json", "AXL Book Catalog Admin API"); });
        }
    }
}