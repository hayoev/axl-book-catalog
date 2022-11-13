using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Common.Configurations;
using Application.Admin.Common.Exceptions;
using Application.Admin.Common.Interfaces;
using Application.Admin.Services.PasswordHasher;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin.Features.Identities.Commands.SetPassword
{
    public class SetPasswordCommandHandler : IRequestHandler<SetPasswordCommand, bool>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly AuthenticationConfiguration _authenticationConfiguration;

        public SetPasswordCommandHandler(
            IApplicationDbContext dbContext,
            AuthenticationConfiguration authenticationConfiguration,
            IPasswordHasherService passwordHasher)
        {
            _dbContext = dbContext;
            _authenticationConfiguration = authenticationConfiguration;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            var adminUser = await _dbContext.AdminUsers
                                .FirstOrDefaultAsync(a => a.Username == request.Username, cancellationToken)
                            ?? throw new LogicException("AdminUser Not Found!");

            var (passwordHash, passwordSalt) = _passwordHasher
                .Create(request.Password, _authenticationConfiguration.Password.GlobalSalt);

            adminUser.Password = passwordHash;
            adminUser.PasswordSalt = passwordSalt;
            adminUser.PasswordExpireDateTime = DateTime.Now
                .AddDays(_authenticationConfiguration.Password.IntervalPasswordExpireDay);

            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}