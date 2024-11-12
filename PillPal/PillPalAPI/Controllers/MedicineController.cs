using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PillPalAPI.Model;
using PillPalAPI.Repositories;
using PillPalLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillPalAPI.Controllers
{
    [Authorize]
    [Route("PillPal/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IItemStore<Medicine> _medicineRepository;
        public MedicineController(IItemStore<Medicine> medicineRepository) {
            _medicineRepository = medicineRepository;
        }
        // GET: api/<MedicineController>
        [HttpGet]
        public IActionResult Get() => Ok(_medicineRepository.GetAll());

        // GET api/<MedicineController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var medicine = _medicineRepository.Get(id);
            if (medicine == null) 
                return NotFound();
            return Ok(medicine);
        }

        // POST api/<MedicineController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Post([FromBody] Medicine medicine)
        {
            if (_medicineRepository.Add(medicine))
                return Ok(medicine);
            return BadRequest("Medicine with this ID already exists.");
        }

        // PUT api/<MedicineController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Medicine medicine)
        {
            if(_medicineRepository.Update(medicine))
                return Ok(medicine);
            return NotFound();
        }

        // DELETE api/<MedicineController>/5
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
