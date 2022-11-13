using System;
using System.Security.Claims;
using Application.Admin.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace WebApi.Admin.Common.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) == null)
            {
                UserId = default;
            }
            else
            {
                UserId = Guid.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
        }

        public Guid UserId { get; }
    }
}