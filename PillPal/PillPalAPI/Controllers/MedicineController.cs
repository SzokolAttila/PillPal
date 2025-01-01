using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PillPalAPI.Model;
using PillPalAPI.Repositories;
using PillPalLib;
using PillPalLib.DTOs.MedicineDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillPalAPI.Controllers
{
    [Authorize]
    [Route("PillPal/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IItemStore<Medicine> _medicineRepository;
        private readonly IValidator<CreateMedicineDto> _validator;
        public MedicineController(IItemStore<Medicine> medicineRepository, IValidator<CreateMedicineDto> validator) {
            _medicineRepository = medicineRepository;
            _validator = validator;
        }
        // GET: PillPal/Medicine
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => Ok(_medicineRepository.GetAll());

        // GET PillPal/Medicine/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var medicine = _medicineRepository.Get(id);
            if (medicine == null) 
                return NotFound();
            return Ok(medicine);
        }

        // POST PillPal/Medicine
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Post([FromBody] CreateMedicineDto medicineDto)
        {
            var result = _validator.Validate(medicineDto);
            if (!result.IsValid)
                return BadRequest(result);

            Medicine medicine = new(medicineDto.Name, medicineDto.Description, medicineDto.Manufacturer, medicineDto.PackageUnit);
            
            if (_medicineRepository.Add(medicine))
                return Ok(medicine);
            return BadRequest("Medicine with this ID already exists.");
        }

        // PUT PillPal/Medicine/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateMedicineDto medicineDto)
        {
            if (_medicineRepository.Get(id) == null)
                return NotFound();
            var result = _validator.Validate(medicineDto);
            if(!result.IsValid)
                return BadRequest(result);
            
            Medicine medicine = new(id, medicineDto.Name, medicineDto.Description, medicineDto.Manufacturer, medicineDto.PackageUnit);

            if (_medicineRepository.Update(medicine))
                return Ok(medicine);
            return BadRequest("Something went wrong.");
        }

        // DELETE PillPal/Medicine/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_medicineRepository.Delete(id))
                return NoContent();   
            return NotFound();
        }
    }
}
