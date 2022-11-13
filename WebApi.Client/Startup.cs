using Application.Client;
using Application.Client.Common.Extensions;
using Application.Client.Common.Interfaces;
using Application.Client.Common.PasswordHasher;
using Application.Client.Features.Books.Queries.GetBooks;
using FluentValidation.AspNetCore;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Client.Common.Extensions;
using WebApi.Client.Common.Filters;
using WebApi.Client.Common.Services;

namespace WebApi.Client
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
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetBooksQuery>());

            
            services.AddApplicationLayer(Configuration); 
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

            

            services.AddApplicationAuthentication(Configuration);
            services.AddWebApiAuthorization();
            services.AddClientPersistenceInfrastructureLayer(Configuration);
            services.AddPasswordHasher();
            services.AddSwagger(Configuration);
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
            app.UseErrorHandler();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint($"/swagger/v1/swagger.json", "AXL Book Catalog Client API"); });
        }
    }
}