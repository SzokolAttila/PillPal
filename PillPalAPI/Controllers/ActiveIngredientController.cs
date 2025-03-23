using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PillPalAPI.Model;
using PillPalAPI.Repositories;
using PillPalLib;
using PillPalLib.DTOs.ActiveIngredientDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillPalAPI.Controllers
{
    [Route("PillPal/[controller]")]
    [ApiController]
    public class ActiveIngredientController : ControllerBase
    {
        private readonly IItemStore<ActiveIngredient> _activeIngredientRepository;
        private readonly IValidator<CreateActiveIngredientDto> _validator;
        public ActiveIngredientController(IItemStore<ActiveIngredient> activeIngredientRepository, IValidator<CreateActiveIngredientDto> validator)
        {
            _activeIngredientRepository = activeIngredientRepository;
            _validator = validator;
        }

        // GET: api/<ActiveIngredientController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => Ok(_activeIngredientRepository.GetAll());

        // GET api/<ActiveIngredientController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var activeIngredient = _activeIngredientRepository.Get(id);
            if (activeIngredient == null)
                return NotFound();
            return Ok(activeIngredient);
        }

        // POST api/<ActiveIngredientController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] CreateActiveIngredientDto createDto)
        {
            var result = _validator.Validate(createDto);
            if (!result.IsValid)
                return BadRequest(result);

            var activeIngredient = new ActiveIngredient()
            {
                Ingredient = createDto.Ingredient
            };

            if (_activeIngredientRepository.Add(activeIngredient))
                return Ok(activeIngredient);
            return BadRequest("ActiveIngredient with this ID already exists.");
        }

        // PUT api/<ActiveIngredientController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] CreateActiveIngredientDto createDto)
        {
            if (_activeIngredientRepository.Get(id) == null)
                return NotFound();
            var result = _validator.Validate(createDto);
            if (!result.IsValid)
                return BadRequest(result);

            var activeIngredient = new ActiveIngredient()
            {
                Id = id,
                Ingredient = createDto.Ingredient
            };

            if (_activeIngredientRepository.Update(activeIngredient))
                return Ok(activeIngredient);
            return BadRequest("Something went wrong.");
        }

        // DELETE api/<ActiveIngredientController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (_activeIngredientRepository.Delete(id))
                return NoContent();
            return NotFound();
        }
    }
}
