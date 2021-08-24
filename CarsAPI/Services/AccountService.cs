using CarsAPI.Entities;
using CarsAPI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPI.Services
{
    public interface IAccountService
    {
        void NewUser(RegisterUserDto dto);
    }

    public class AccountService : IAccountService
    {
        private readonly CarDbContext context;
        private readonly IPasswordHasher<User> hasher;

        public AccountService(CarDbContext context, IPasswordHasher<User> hasher)
        {
            this.context = context;
            this.hasher = hasher;
        }
        public void NewUser(RegisterUserDto dto)
        {
            User user = new User()
            {
                Email = dto.Email,
                Login = dto.Login,
                RoleId = dto.RoleId
            };
            var hashPassword = hasher.HashPassword(user, dto.Password);
            user.PasswordHash = hashPassword;

            context.Users.Add(user);
            context.SaveChanges();

        }
    }
}
