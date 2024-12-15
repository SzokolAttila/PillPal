using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PillPalAPI.Model;
using PillPalAPI.Repositories;
using PillPalLib;
using PillPalLib.DTOs.MedicineActiveIngredientDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillPalAPI.Controllers
{
    [Route("PillPal/[controller]")]
    [ApiController]
    public class MedicineActiveIngredientController : ControllerBase
    {
        // GET: api/<MedicineActiveIngredientController>
        private readonly IJoinStore<MedicineActiveIngredient> _joinRepository;
        private readonly IItemStore<ActiveIngredient> _activeIngredientRepository;
        private readonly IItemStore<Medicine> _medicineRepository;
        public MedicineActiveIngredientController(
            IJoinStore<MedicineActiveIngredient> joinRepository, 
            IItemStore<ActiveIngredient> activeIngredientRepository, 
            IItemStore<Medicine> medicineRepository)
        {
            _joinRepository = joinRepository;
            _activeIngredientRepository = activeIngredientRepository;
            _medicineRepository = medicineRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => Ok(_joinRepository.GetAll());

        // GET api/<MedicineActiveIngredientController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id) => Ok(_joinRepository.Get(id));

        // POST api/<MedicineActiveIngredientController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] CreateMedicineActiveIngredientDto createDto)
        {
            if (_medicineRepository.Get(createDto.MedicineId) == null)
                return BadRequest("Medicine with the given ID doesn't exist.");
            if (_activeIngredientRepository.Get(createDto.ActiveIngredientId) == null)
                return BadRequest("ActiveIngredient with the given ID doesn't exist.");

            if (_joinRepository.Get(createDto.MedicineId).FirstOrDefault(x => x.ActiveIngredientId == createDto.ActiveIngredientId) != null)
                return BadRequest("This ActiveIngredient has already been added to this Medicine.");

            var activeIngredient = new MedicineActiveIngredient()
            {
                MedicineId = createDto.MedicineId,
                ActiveIngredientId = createDto.ActiveIngredientId,
            };

            if (_joinRepository.Add(activeIngredient))
                return Ok(activeIngredient);
            return BadRequest("MedicineActiveIngredient with the given ID already exists.");
        }

        // PUT api/<MedicineActiveIngredientController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] CreateMedicineActiveIngredientDto createDto)
        {
            if (_joinRepository.GetAll().FirstOrDefault(x => x.Id == id) == null)
                return NotFound();
            if (_medicineRepository.Get(createDto.MedicineId) == null)
                return BadRequest("Medicine with the given ID doesn't exist.");
            if (_activeIngredientRepository.Get(createDto.ActiveIngredientId) == null)
                return BadRequest("ActiveIngredient with the given ID doesn't exist.");


            if (_joinRepository.Get(createDto.MedicineId).FirstOrDefault(x => x.ActiveIngredientId == createDto.ActiveIngredientId) != null)
                return BadRequest("This ActiveIngredient has already been added to this Medicine.");


            var activeIngredient = new MedicineActiveIngredient()
            {
                Id = id,
                ActiveIngredientId = createDto.ActiveIngredientId,
                MedicineId = createDto.MedicineId,
            };

            if (_joinRepository.Update(activeIngredient))
                return Ok(activeIngredient);
            return BadRequest("Something went wrong.");
        }

        // DELETE api/<MedicineActiveIngredientController>/5
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
