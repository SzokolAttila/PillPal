using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PillPalAPI.Model;
using PillPalAPI.Options;
using PillPalLib;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PillPalAPI.Controllers
{
    [Route("api/[controller]")]
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

        // POST api/<LoginController>
        [HttpPost]
        public IActionResult Login([FromBody] Login loginUser)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.UserName == loginUser.UserName);
            if (user == null)
            {
                return BadRequest("Invalid username or password.");
            }
            if (!user.Matches(loginUser.Password))
            {
                return BadRequest("Invalid username or password.");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claim = new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, "User")
            };

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claim,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(tokenStr);
        }
    }
}
