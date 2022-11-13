using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Client.Common.Configurations;
using Application.Client.Common.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace Application.Client.Common.Token
{
    public class TokenService : ITokenService
    {
        private readonly AuthenticationConfiguration _authenticationConfiguration;

        public TokenService(AuthenticationConfiguration authenticationConfiguration)
        {
            _authenticationConfiguration = authenticationConfiguration;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = _authenticationConfiguration.Jwt.Key;
            var issuer = _authenticationConfiguration.Jwt.Issuer;
            var audience = _authenticationConfiguration.Jwt.Audience;
            var timeOutMinutes = _authenticationConfiguration.TimeoutAccessTokenMinutes;
            
            return GenerateJwnToken(claims, key, issuer, audience,timeOutMinutes);
        }
        
        public string GenerateTempToken(IEnumerable<Claim> claims)
        {
            var suffix = "_temp";
            var key = $"{_authenticationConfiguration.Jwt.Key}{suffix}";
            var issuer = $"{_authenticationConfiguration.Jwt.Issuer}{suffix}";
            var audience = $"{_authenticationConfiguration.Jwt.Audience}{suffix}";
            var timeOutMinutes = _authenticationConfiguration.TimeoutTempTokenMinutes;
            
            return GenerateJwnToken(claims, key, issuer, audience, timeOutMinutes);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[256];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = _authenticationConfiguration.Jwt.Key;
            var issuer = _authenticationConfiguration.Jwt.Issuer;
            var audience = _authenticationConfiguration.Jwt.Audience;
            return GetPrincipalFromToken(token, key, issuer, audience, false);
        }
        
        public ClaimsPrincipal GetPrincipalTempToken(string token)
        {
            var suffix = "_temp";
            var key = $"{_authenticationConfiguration.Jwt.Key}{suffix}";
            var issuer = $"{_authenticationConfiguration.Jwt.Issuer}{suffix}";
            var audience = $"{_authenticationConfiguration.Jwt.Audience}{suffix}";
            
            return GetPrincipalFromToken(token, key, issuer, audience, false);
        }
        
        private string GenerateJwnToken(IEnumerable<Claim> claims, string key, string issuer, string audience, int timeOutMinutes)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = now,
                Expires = now.Add(TimeSpan.FromMinutes(timeOutMinutes)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private ClaimsPrincipal GetPrincipalFromToken(string token, string key, string issuer, string audience, bool validateLifetime = true)
        {
            try
            {
                var keyBytes = Encoding.ASCII.GetBytes(key);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateLifetime = validateLifetime
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                // ReSharper disable once InlineOutVariableDeclaration
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                    throw new UnauthorizedException();

                return principal;
            }
            catch (Exception)
            {
                throw new UnauthorizedException();
            }
        }
    }
}