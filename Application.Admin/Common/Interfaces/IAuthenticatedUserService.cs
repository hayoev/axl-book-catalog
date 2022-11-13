using System;

namespace Application.Admin.Common.Interfaces
{
    public interface IAuthenticatedUserService
    {
        Guid UserId { get; }
    }
}