using System;
using System.Security.Claims;
using Application.Client.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace WebApi.Client.Common.Services
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