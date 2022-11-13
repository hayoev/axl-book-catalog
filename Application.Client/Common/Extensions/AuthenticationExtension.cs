using System;
using System.Text;
using System.Threading.Tasks;
using Application.Client.Common.Configurations;
using Application.Client.Common.Exceptions;
using Application.Client.Common.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Application.Client.Common.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddApplicationAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            var authenticationConfiguration = new AuthenticationConfiguration();
            configuration.GetSection(AuthenticationConfiguration.Key).Bind(authenticationConfiguration);
            services.AddSingleton(authenticationConfiguration);
            
            if (authenticationConfiguration.Jwt?.Key == null || authenticationConfiguration.Jwt?.Issuer == null ||
                authenticationConfiguration.Jwt?.Audience == null)
            {
                throw new Exception("Section 'Authentication.Jwt' configuration settings are not found in settings file");
            }
            
            if (string.IsNullOrEmpty(authenticationConfiguration.Password?.GlobalSalt))
            {
                throw new Exception("Section 'Authentication.Password.GlobalSalt' configuration settings are not found in settings file");
            }
            
            if (!authenticationConfiguration.PasswordExpireControl.HasValue)
            {
                throw new Exception("Section 'Authentication.PasswordExpireControl' configuration settings are not found in settings file");
            }
            
            services.AddTransient<ITokenService, TokenService>();

            // Configure jwt authentication
            var key = Encoding.ASCII.GetBytes(authenticationConfiguration.Jwt.Key);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = authenticationConfiguration.Jwt.Issuer,
                        ValidAudience = authenticationConfiguration.Jwt.Audience,
                        ClockSkew = TimeSpan.FromMinutes(0)
                    };
                    x.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                throw new TokenExpiredException();
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}