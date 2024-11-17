using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PillPalAPI.Model;
using PillPalLib;
using PillPalLib.DTOs.UserDTOs;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillPalAPI.Controllers
{
    [Route("PillPal/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IItemStore<User> _userRepository;
        private readonly IValidator<CreateUserDto> _validator;
        private readonly IValidator<string> _passwordValidator;
        public UserController(IItemStore<User> userRepository, IValidator<CreateUserDto> validator, IValidator<string> passwordValidator)
        {
            _validator = validator;
            _userRepository = userRepository;
            _passwordValidator = passwordValidator;
        }
        // GET: api/<UserController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get() => Ok(_userRepository.GetAll());

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (HttpContext.User.Identity is not ClaimsIdentity identity)
                return BadRequest("Something went wrong.");
            var identitySid = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            if (identitySid != id.ToString())
            {
                var isAdmin = identity.Claims.Where(x => x.Type == ClaimTypes.Role && x.Value != null).Any(x => x.Value! == "Admin");
                if (!isAdmin)
                    return Forbid();
            }

            var user = _userRepository.Get(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserDto userDto)
        {
            var result = _validator.Validate(userDto);
            if (!result.IsValid)
                return BadRequest(result);
            var user = new User(userDto.UserName, userDto.Password);
            if (_userRepository.Add(user))
                return Ok(user);
            return BadRequest("User with this ID already exists.");
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] CreateUserDto userDto)
        {
            if (HttpContext.User.Identity is not ClaimsIdentity identity)
                return BadRequest("Something went wrong.");
            var identitySid = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            if (identitySid != id.ToString())
            {
                var isAdmin = identity.Claims.Where(x => x.Type == ClaimTypes.Role && x.Value != null).Any(x => x.Value! == "Admin");
                if (!isAdmin)
                    return Forbid();
            }

            var pastUser = _userRepository.Get(id);
            if (pastUser == null)
                return NotFound();
            if (userDto.UserName == pastUser.UserName)
            {
                var passwordResult = _passwordValidator.Validate(userDto.Password);
                if (!passwordResult.IsValid)
                    return BadRequest(passwordResult);
            }    
            else
            {
                var result = _validator.Validate(userDto);
                if (!result.IsValid)
                    return BadRequest(result);
            }

            var user = new User(id, userDto.UserName, userDto.Password);
            if (_userRepository.Update(user))
                return Ok(user);
            return BadRequest("Something went wrong.");
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (HttpContext.User.Identity is not ClaimsIdentity identity)
                return BadRequest("Something went wrong.");
            var identitySid = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            if (identitySid != id.ToString())
            {
                var isAdmin = identity.Claims.Where(x => x.Type == ClaimTypes.Role && x.Value != null).Any(x => x.Value! == "Admin");
                if (!isAdmin)
                    return Forbid();
            }

            if (_userRepository.Delete(id))
                return NoContent();
            return NotFound();
        }
    }
}
