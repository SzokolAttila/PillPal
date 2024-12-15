using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PillPalAPI.Model;
using PillPalAPI.Repositories;
using PillPalLib;
using PillPalLib.DTOs.PackageSizeDTOs;
using SQLitePCL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillPalAPI.Controllers
{
    [Route("PillPal/[controller]")]
    [ApiController]
    public class PackageSizeController : ControllerBase
    {
        private readonly IJoinStore<PackageSize> _joinRepository;
        private readonly IValidator<CreatePackageSizeDto> _validator;
        private readonly IItemStore<Medicine> _medicineRepository;
        public PackageSizeController(
            IJoinStore<PackageSize> joinRepository, 
            IValidator<CreatePackageSizeDto> validator, 
            IItemStore<Medicine> medicineRepository)
        {
            _joinRepository = joinRepository;
            _validator = validator;
            _medicineRepository = medicineRepository;
        }

        // GET: api/<PackageSizeController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => Ok(_joinRepository.GetAll());
        // GET api/<PackageSizeController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var result = _joinRepository.Get(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST api/<PackageSizeController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] CreatePackageSizeDto createDto)
        {
            if (_medicineRepository.Get(createDto.MedicineId) == null)
                return BadRequest("Medicine with the given ID doesn't exist.");

            if (_joinRepository.Get(createDto.MedicineId).FirstOrDefault(x => x.Size == createDto.Size) != null)
                return BadRequest("This PackageSize has already been added to this Medicine.");

            var result = _validator.Validate(createDto);
            if (!result.IsValid)
                return BadRequest(result);

            var packageSize = new PackageSize()
            {
                MedicineId = createDto.MedicineId,
                Size = createDto.Size,
            };

            if (_joinRepository.Add(packageSize))
                return Ok(packageSize);
            return BadRequest("PackageSize with the given ID already exists.");
        }

        // PUT api/<PackageSizeController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] CreatePackageSizeDto createDto)
        {
            if (_joinRepository.GetAll().FirstOrDefault(x => x.Id == id) == null)
                return NotFound();
            if (_medicineRepository.Get(createDto.MedicineId) == null)
                return BadRequest("Medicine with the given ID doesn't exist.");

            if (_joinRepository.Get(createDto.MedicineId).FirstOrDefault(x => x.Size == createDto.Size) != null)
                return BadRequest("This PackageSize has already been added to this Medicine.");

            var result = _validator.Validate(createDto);
            if (!result.IsValid)
                return BadRequest(result);

            var packageSize = new PackageSize()
            {
                Id = id,
                Size = createDto.Size,
                MedicineId = createDto.MedicineId,
            };

            if (_joinRepository.Update(packageSize))
                return Ok(packageSize);
            return BadRequest("Something went wrong.");
        }

        // DELETE api/<PackageSizeController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (_joinRepository.Delete(id))
                return NoContent();
            return NotFound();
        }
    }
}
