﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PillPalAPI.Model;
using PillPalAPI.Options;
using PillPalLib;
using PillPalLib.DTOs.UserDTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PillPalAPI.Controllers
{
    [AllowAnonymous]
    [Route("PillPal/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IItemStore<User> _userRepository;
        private readonly JwtOptions jwtOptions;
        public LoginController(IItemStore<User> userRepository, IOptions<JwtOptions> options)
        {
            _userRepository = userRepository;
            jwtOptions = options.Value;
        }

        // POST PillPal/Login
        [HttpPost]
        public IActionResult Login([FromBody] CreateUserDto loginUser)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.UserName == loginUser.UserName);
            if (user == null)
            {
                return BadRequest("Hibás felhasználónév vagy jelszó.");
            }
            if (!user.Matches(loginUser.Password))
            {
                return BadRequest("Hibás felhasználónév vagy jelszó.");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claim = new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, loginUser.UserName == "administrator" ? "Admin" : "User")
            };

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claim,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { Id = user.Id, Token = tokenStr });
        }
    }
}
