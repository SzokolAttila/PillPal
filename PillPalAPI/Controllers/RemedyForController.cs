using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PillPalAPI.Model;
using PillPalAPI.Repositories;
using PillPalLib;
using PillPalLib.DTOs.RemedyForDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillPalAPI.Controllers
{
    [Route("PillPal/[controller]")]
    [ApiController]
    public class RemedyForController : ControllerBase
    {
        private readonly IItemStore<RemedyFor> _remedyForRepository;
        private readonly IValidator<CreateRemedyForDto> _validator;
        public RemedyForController(IItemStore<RemedyFor> repository, IValidator<CreateRemedyForDto> validator) { 
            _remedyForRepository = repository;
            _validator = validator;
        }
        // GET: api/<RemedyForController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => Ok(_remedyForRepository.GetAll());

        // GET api/<RemedyForController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var result = _remedyForRepository.Get(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST api/<RemedyForController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] CreateRemedyForDto createDto)
        {
            var result = _validator.Validate(createDto);
            if (!result.IsValid)
                return BadRequest(result);

            var remedyFor = new RemedyFor()
            {
                Ailment = createDto.Ailment
            };

            if (_remedyForRepository.Add(remedyFor))
                return Ok(remedyFor);
            return BadRequest("Létezik már betegség ezzel az ID-val.");
        }

        // PUT api/<RemedyForController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] CreateRemedyForDto createDto)
        {
            if (_remedyForRepository.Get(id) == null)
                return NotFound();
            var result = _validator.Validate(createDto);
            if (!result.IsValid)
                return BadRequest(result);

            var remedyFor = new RemedyFor()
            {
                Id = id,
                Ailment = createDto.Ailment
            };

            if (_remedyForRepository.Update(remedyFor))
                return Ok(remedyFor);
            return BadRequest("Valami hiba történt.");
        }

        // DELETE api/<RemedyForController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (_remedyForRepository.Delete(id))
                return NoContent();
            return NotFound();
        }
    }
}
