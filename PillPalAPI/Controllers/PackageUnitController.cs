using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PillPalAPI.Model;
using PillPalAPI.Repositories;
using PillPalLib;
using PillPalLib.DTOs.PackageUnitDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillPalAPI.Controllers
{
    [Route("PillPal/[controller]")]
    [ApiController]
    public class PackageUnitController : ControllerBase
    {
        private readonly IItemStore<PackageUnit> _packageUnitRepository;
        private readonly IValidator<CreatePackageUnitDto> _validator;
        public PackageUnitController(IItemStore<PackageUnit> repository, IValidator<CreatePackageUnitDto> validator) { 
            _packageUnitRepository = repository;
            _validator = validator;
        }
        // GET: api/<PackageUnit>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => Ok(_packageUnitRepository.GetAll());

        // GET api/<PackageUnit>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var result = _packageUnitRepository.Get(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST api/<PackageUnit>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] CreatePackageUnitDto createDto)
        {
            var result = _validator.Validate(createDto);
            if (!result.IsValid)
                return BadRequest(result);

            var packageUnit = new PackageUnit()
            {
                Name = createDto.Name
            };

            if (_packageUnitRepository.Add(packageUnit))
                return Ok(packageUnit);
            return BadRequest("Létezik már mértékegység ezzel az ID-val.");
        }

        // PUT api/<PackageUnit>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] CreatePackageUnitDto createDto)
        {
            if (_packageUnitRepository.Get(id) == null)
                return NotFound();
            var result = _validator.Validate(createDto);
            if (!result.IsValid)
                return BadRequest(result);

            var packageUnit = new PackageUnit()
            {
                Id = id,
                Name = createDto.Name
            };

            if (_packageUnitRepository.Update(packageUnit))
                return Ok(packageUnit);
            return BadRequest("Valami hiba történt.");
        }

        // DELETE api/<PackageUnit>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (_packageUnitRepository.Delete(id))
                return NoContent();
            return NotFound();
        }
    }
}
