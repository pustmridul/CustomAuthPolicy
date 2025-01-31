﻿using CustomAuthPolicy.Data;
using CustomAuthPolicy.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace CustomAuthPolicy.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(AppDbContext context, IJwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            var user = _context.Users
                .Include(i=>i.UserRoles).ThenInclude(i=>i.Role)
                .SingleOrDefault(u => u.UserName == loginModel.UserName && u.Password == loginModel.Password); // Assume you have a Password field

            if (user == null)
            {
                return Unauthorized();
            }
       
            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(_jwtTokenService.GenerateJWToken(user)) });
        }
    }

    public class LoginModel
    {
        public string UserName { get; set; }= string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}