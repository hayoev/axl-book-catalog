using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Client.Common.Configurations;
using Application.Client.Common.Exceptions;
using Application.Client.Common.Interfaces;
using Application.Client.Common.PasswordHasher;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Client.Features.Identities.Commands.SetPassword
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
            var user = await _dbContext.Users
                                .FirstOrDefaultAsync(a => a.Username == request.Username, cancellationToken)
                            ?? throw new LogicException("User not found");

            var (passwordHash, passwordSalt) = _passwordHasher
                .Create(request.Password, _authenticationConfiguration.Password.GlobalSalt);

            user.Password = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordExpireDateTime = DateTime.Now
                .AddDays(_authenticationConfiguration.Password.IntervalPasswordExpireDay);

            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}