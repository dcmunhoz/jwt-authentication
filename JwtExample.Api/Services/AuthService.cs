using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtExample.Api.DTOs;
using JwtExample.Api.Entities;
using Microsoft.IdentityModel.Tokens;

namespace JwtExample.Api.Services
{
    public class AuthService : IAuthService
    {
        
        public string Authenticate(string key, string issuer, UserDTO user)
        {
            // var claims = new[]
            // {
            //     new Claim(ClaimTypes.Name, user.Username.ToString()),
            //     new Claim(ClaimTypes.Role, user.Role.ToString()),
            //     new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            // };
            //
            // var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            // var credential = new SigningCredentials(security, SecurityAlgorithms.HmacSha256Signature);
            // var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: credential);
            //
            // return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);  

            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }

        public bool ValidateAuth(string key, string issuer, string token)
        {

            var secret = Encoding.ASCII.GetBytes(key);
            var securityKey = new SymmetricSecurityKey(secret);
            var tokenHandler = new JwtSecurityTokenHandler();


            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;

        }
        
    }
}