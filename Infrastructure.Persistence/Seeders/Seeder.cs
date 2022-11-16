using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Admin.Common.Configurations;
using Application.Admin.Common.Interfaces;
using Application.Admin.Services.PasswordHasher;
using Infrastructure.Persistence.Seeds.Base;
using Microsoft.AspNetCore.Hosting;
using Npgsql;

namespace Infrastructure.Persistence.Seeders
{
    public class Seeder : ISeeder
    {
        private readonly Dictionary<string, string> _messages = new Dictionary<string, string>();
        private readonly IApplicationDbContext _applicationDbContext;
        protected readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly AuthenticationConfiguration _authenticationConfiguration;

        public Seeder(IApplicationDbContext applicationDbContext, IWebHostEnvironment environment, IPasswordHasherService passwordHasher, AuthenticationConfiguration authenticationConfiguration)
        {
            _applicationDbContext = applicationDbContext;
            _webHostEnvironment = environment;
            _passwordHasher = passwordHasher;
            _authenticationConfiguration = authenticationConfiguration;
        }
        
        public Dictionary<string, string> GetMessages()
        {
            return _messages;
        }

        public async Task Start(bool isForceUpdate = false)
        {
            var seeds = typeof(BaseSeed<>)
                .Assembly.GetTypes()
                .Where(x => !x.IsAbstract && !x.IsInterface && x.BaseType != null && x.BaseType.IsGenericType &&
                            x.BaseType.GetGenericTypeDefinition() == typeof(BaseSeed<>))
                .Select(t => (IBaseSeed) Activator.CreateInstance(t, _passwordHasher, _authenticationConfiguration));

            foreach (var seed in seeds)
            {
                try
                {
                    if (seed == null) continue;
                    seed.SetDbContext(_applicationDbContext);
                    seed.SetWebHostEnvironment(_webHostEnvironment);
                    await seed.Start(isForceUpdate);
                    _messages.Add(seed.GetType().Name, "Success");
                }
                catch (PostgresException e)
                {
                    if (seed != null)
                        _messages.Add(seed.GetType().Name, e.Message);
                    throw e;
                }
            }
        }
    }
}