using System;
using System.Collections.Generic;
using System.Linq;
using JwtExample.Api.Entities;

namespace JwtExample.Api.Repository
{
    public class UserRepository: IUserRepository
    {

        public List<User> Users;


        public UserRepository()
        {
            Users = new List<User>();
            
            Users.Add(new User
            {
                Id = 1,
                Username = "daniel1",
                Password = "123456",
                Role = "Admin"
            });
            
            Users.Add(new User
            {
                Id = 2,
                Username = "daniel2",
                Password = "dasdasdsa",
                Role = "Admin"
            });
            
            Users.Add(new User
            {
                Id = 3,
                Username = "daniel3",
                Password = "12342132156",
                Role = "User"
            });
            
            Users.Add(new User
            {
                Id = 4,
                Username = "daniel4",
                Password = "12vcxvxcv3456",
                Role = "User"
            });
        }
        
        public User GetUser(string username, string password)
        {
            return Users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
        }

        public List<User> List()
        {
            return Users;
        } 
    }
}