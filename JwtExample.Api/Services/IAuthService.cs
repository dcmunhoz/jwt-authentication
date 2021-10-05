using JwtExample.Api.DTOs;

namespace JwtExample.Api.Services
{
    public interface IAuthService
    {
        public string Authenticate(string key, string issuer, UserDTO userDto);
        public bool ValidateAuth(string key, string issuer, string token);
    }
}