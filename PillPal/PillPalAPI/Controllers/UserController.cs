using Microsoft.AspNetCore.Mvc;
using PillPalAPI.DTOs.UserDTOs;
using PillPalAPI.Model;
using PillPalLib;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IItemStore<User> _userRepository;
        public UserController(IItemStore<User> userRepository)
        {
            _userRepository = userRepository;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get() => Ok(_userRepository.GetAll());

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userRepository.Get(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserDto userDto)
        {
            var user = new User(userDto.UserName, userDto.PasswordText);
            if (_userRepository.Add(user))
                return Ok(user);
            return BadRequest("User with this ID already exists.");
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateUserDto userDto)
        {
            var user = new User(userDto.UserName, userDto.PasswordText);
            if (_userRepository.Update(user))
                return Ok(user);
            return NotFound();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_userRepository.Delete(id))
                return NoContent();
            return NotFound();
        }
    }
}
