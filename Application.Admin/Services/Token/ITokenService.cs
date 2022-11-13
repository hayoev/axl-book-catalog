using System.Collections.Generic;
using System.Security.Claims;

namespace Application.Admin.Services.Token
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateTempToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        ClaimsPrincipal GetPrincipalTempToken(string token);
    }
}