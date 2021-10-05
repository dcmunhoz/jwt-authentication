using System;
using System.Collections.Generic;
using JwtExample.Api.DTOs;
using JwtExample.Api.Entities;


namespace JwtExample.Api.Repository
{
    public interface IUserRepository
    {
        public User GetUser(string username, string password);
        public List<User> List();
    }
}