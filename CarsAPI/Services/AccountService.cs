using CarsAPI.Entities;
using CarsAPI.Exceptions;
using CarsAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarsAPI.Services
{
    public interface IAccountService
    {
        void NewUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
    }

    public class AccountService : IAccountService
    {
        private readonly CarDbContext context;
        private readonly IPasswordHasher<User> hasher;
        private readonly AuthenticationSettings authenticationSettings;

        public AccountService(CarDbContext context, IPasswordHasher<User> hasher, AuthenticationSettings authenticationSettings)
        {
            this.context = context;
            this.hasher = hasher;
            this.authenticationSettings = authenticationSettings;
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

        public string GenerateJwt(LoginDto dto)
        {
            var user = context.Users
                .Include(u => u.Roles)
                .FirstOrDefault(u => u.Email == dto.Email);

            if (user is null)
                throw new BadRequestException("Invalid username or password");

            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Login}"),
                new Claim(ClaimTypes.Role, $"{user.Roles.Name}")

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(authenticationSettings.JwtIssuer,
                authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
