using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PillPalAPI.Model;
using PillPalAPI.Repositories;
using PillPalLib;
using PillPalLib.DTOs.MedicineRemedyForDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillPalAPI.Controllers
{
    [Route("PillPal/[controller]")]
    [ApiController]
    public class MedicineRemedyForController : ControllerBase
    {
        private readonly IJoinStore<MedicineRemedyFor> _joinRepository;
        private readonly IItemStore<Medicine> _medicineRepository;
        private readonly IItemStore<RemedyFor> _remedyForRepository;
        public MedicineRemedyForController(
            IJoinStore<MedicineRemedyFor> joinRepository, 
            IItemStore<Medicine> medicineRepository, 
             IItemStore<RemedyFor> remedyForRepository)
        {
            _joinRepository = joinRepository;
            _medicineRepository = medicineRepository;
            _remedyForRepository = remedyForRepository;
        }

        // GET: api/<MedicineRemedyForController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => Ok(_joinRepository.GetAll());

        // GET api/<MedicineRemedyForController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id) => Ok(_joinRepository.Get(id));

        // POST api/<MedicineRemedyForController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] CreateMedicineRemedyForDto createDto)
        {
            if (_medicineRepository.Get(createDto.MedicineId) == null)
                return BadRequest("Nem létezik gyógyszer a megadott ID-val.");
            if (_remedyForRepository.Get(createDto.RemedyForId) == null)
                return BadRequest("Nem létezik betegség a megadott ID-val.");

            var remedyFor = new MedicineRemedyFor()
            {
                MedicineId = createDto.MedicineId,
                RemedyForId = createDto.RemedyForId,
            };

            if (_joinRepository.Add(remedyFor))
                return Ok(remedyFor);
            return BadRequest("Létezik már gyógyszer-betegség a megadott ID-val.");
        }

        // PUT api/<MedicineRemedyForController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] CreateMedicineRemedyForDto createDto)
        {
            if (_joinRepository.GetAll().FirstOrDefault(x => x.Id == id) == null)
                return NotFound();
            if (_medicineRepository.Get(createDto.MedicineId) == null)
                return BadRequest("Nem létezik gyógyszer a megadott ID-val.");
            if (_remedyForRepository.Get(createDto.RemedyForId) == null)
                return BadRequest("Nem létezik betegség a megadott ID-val.");

            var remedyFor = new MedicineRemedyFor()
            {
                Id = id,
                RemedyForId = createDto.RemedyForId,
                MedicineId = createDto.MedicineId,
            };

            if (_joinRepository.Update(remedyFor))
                return Ok(remedyFor);
            return BadRequest("Valami hiba történt.");
        }

        // DELETE api/<MedicineRemedyForController>/5
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
