using System;
using System.Linq;
using JwtExample.Api.DTOs;
using JwtExample.Api.Entities;
using JwtExample.Api.Repository;
using JwtExample.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JwtExample.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class AppController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;

        public AppController(IUserRepository userRepo, IAuthService authService, IConfiguration config)
        {
            this._userRepo = userRepo;
            this._authService = authService;
            this._config = config;
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public IActionResult Login([FromBody] UserLoginInputModel user)
        {

            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest();
            }

            User u = _userRepo.GetUser(user.Username, user.Password);
            
            if (u == null)
            {
                return NotFound("User or password incorrect !");
            }

            UserDTO udto = new UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                Password = u.Password,
                Role = u.Role
            };


            string token =
                _authService.Authenticate(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), udto);
            
            
            return Ok(token);
        }
        
        [HttpGet("/teste")]
        [Authorize(Roles = "User")]                               
        public IActionResult GetTest()
        {
            return Ok($"Funcionou =D \nAutenticado como: {User.Identity.Name} \nID:{User.Identity.Name} .");
        }

        [HttpGet("/admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminRoute()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ").Last();

            var validated = _authService.ValidateAuth(_config["Jwt:Key"].ToString(), "", token);
            

            return Ok(validated);
        }
        
    }
}
