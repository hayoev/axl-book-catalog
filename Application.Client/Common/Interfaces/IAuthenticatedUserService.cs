using System;

namespace Application.Client.Common.Interfaces
{
    public interface IAuthenticatedUserService
    {
        Guid UserId { get; }
    }
}