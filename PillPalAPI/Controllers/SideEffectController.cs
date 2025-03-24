using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PillPalAPI.Model;
using PillPalLib;
using PillPalLib.DTOs.SideEffectDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillPalAPI.Controllers
{
    [Route("PillPal/[controller]")]
    [ApiController]
    public class SideEffectController : ControllerBase
    {
        private readonly IItemStore<SideEffect> _sideEffectRepository;
        private readonly IValidator<CreateSideEffectDto> _validator;
        public SideEffectController(IItemStore<SideEffect> sideEffectRepository, IValidator<CreateSideEffectDto> validator)
        {
            _sideEffectRepository = sideEffectRepository;
            _validator = validator;
        }

        // GET: api/<SideEffectController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => Ok(_sideEffectRepository.GetAll());

        // GET api/<SideEffectController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var result = _sideEffectRepository.Get(id);
            if (result == null) 
                return NotFound();
            return Ok(result);
        }

        // POST api/<SideEffectController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] CreateSideEffectDto sideEffectDto)
        {
            var result = _validator.Validate(sideEffectDto);
            if(!result.IsValid)
                return BadRequest(result);

            var sideEffect = new SideEffect()
            {
                Effect = sideEffectDto.Effect
            };

            if (_sideEffectRepository.Add(sideEffect))
                return Ok(sideEffect);
            return BadRequest("SideEffect with this ID already exists.");
        }

        // PUT api/<SideEffectController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] CreateSideEffectDto sideEffectDto)
        {
            if (_sideEffectRepository.Get(id) == null)
                return NotFound();
            var result = _validator.Validate(sideEffectDto);
            if (!result.IsValid)
                return BadRequest(result);

            var sideEffect = new SideEffect()
            {
                Id = id,
                Effect = sideEffectDto.Effect
            };

            if (_sideEffectRepository.Update(sideEffect))
                return Ok(sideEffect);
            return BadRequest("Something went wrong.");
        }

        // DELETE api/<SideEffectController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (_sideEffectRepository.Delete(id))
                return NoContent();
            return NotFound();
        }
    }
}
