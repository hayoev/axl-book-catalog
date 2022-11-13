using System;
using MediatR;

namespace Application.Admin.Features.Identities.Commands.RevokeRefreshToken
{
    public class RevokedRefreshTokenEvent : INotification
    {
        public RevokedRefreshTokenEvent(Guid adminUserId)
        {
            AdminUserId = adminUserId;
        }

        public Guid AdminUserId { get; }
    }
}