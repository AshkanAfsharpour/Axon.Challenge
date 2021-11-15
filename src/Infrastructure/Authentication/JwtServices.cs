using Axon.Application.Common.Interfaces;
using Axon.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Axon.Infrastructure.Authentication
{
    public class JwtServices : IjwtServices
    {
        private readonly JwtConfigurations _jwtConfigurations;

        public JwtServices(JwtConfigurations jwtConfigurations)
        {
            _jwtConfigurations = jwtConfigurations;
        }
       
        public string IssueToken(Guid identifier, int roleId, Guid sessionId)
        {
            SigningCredentials siginingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigurations.Secret)), SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtConfigurations.Issuer,
                audience: _jwtConfigurations.Audience,
                expires: DateTime.UtcNow.AddYears(1),
                signingCredentials: siginingCredentials,
                claims: new List<Claim>
                        {
                            
                            new Claim(JwtRegisteredClaimNames.Jti, sessionId.ToString()),
                            // Issued At format is Unix Timestamp
                            new Claim(JwtRegisteredClaimNames.Iat,((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString()),
                            new Claim(ClaimTypes.Role,UserRoleTypes.GetRoleNameById(roleId)),
                            new Claim(JwtRegisteredClaimNames.Sub,identifier.ToString())
                        },
                  notBefore: DateTime.UtcNow
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[40];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                string finalRefreshToken = Convert.ToBase64String(randomNumber) + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds().ToString();
                return finalRefreshToken;
            }
        }

    }
}