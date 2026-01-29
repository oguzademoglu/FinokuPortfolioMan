using Azure.Core;
using Finoku.Application.DTOs;
using Finoku.Application.Interfaces;
using Finoku.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Finoku.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;      
        public AuthService (AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public string Login(LoginRequestDto request)
        {
            // Admin: admin / 123
            // User: user / 123
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);
            // buraya tekrak bakmak gerek
            if (user == null)
            {
                return null;
            }

            var role = user.Role.ToString();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, role) // Rol token'a gömülü olacak
            };

            var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
