using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PillPalAPI.Model;
using PillPalLib;
using PillPalLib.DTOs.MedicineSideEffectDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillPalAPI.Controllers
{
    [Route("PillPal/[controller]")]
    [ApiController]
    public class MedicineSideEffectController : ControllerBase
    {
        private readonly IJoinStore<MedicineSideEffect> _joinRepository;
        private readonly IItemStore<Medicine> _medicineRepository;
        private readonly IItemStore<SideEffect> _sideEffectRepository;
        public MedicineSideEffectController(
            IJoinStore<MedicineSideEffect> joinRepository, 
            IItemStore<Medicine> medicineRepository, 
            IItemStore<SideEffect> sideEffectRepository)
        {
            _joinRepository = joinRepository;
            _sideEffectRepository = sideEffectRepository;
            _medicineRepository = medicineRepository;
        }

        // GET: api/<MedicineSideEffectController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => Ok(_joinRepository.GetAll());

        // GET api/<MedicineSideEffectController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id) => Ok(_joinRepository.Get(id));

        // POST api/<MedicineSideEffectController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] CreateMedicineSideEffectDto createDto)
        {
            if (_medicineRepository.Get(createDto.MedicineId) == null)
                return BadRequest("Medicine with the given ID doesn't exist.");
            if (_sideEffectRepository.Get(createDto.SideEffectId) == null)
                return BadRequest("SideEffect with the given ID doesn't exist.");

            if (_joinRepository.Get(createDto.MedicineId).FirstOrDefault(x => x.SideEffectId == createDto.SideEffectId) != null)
                return BadRequest("This SideEffect has already been added to this Medicine.");

            var sideEffect = new MedicineSideEffect()
            {
                MedicineId = createDto.MedicineId,
                SideEffectId = createDto.SideEffectId,
            };

            if (_joinRepository.Add(sideEffect))
                return Ok(sideEffect);
            return BadRequest("MedicineSideEffect with the given ID already exists.");
        }

        // PUT api/<MedicineSideEffectController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] CreateMedicineSideEffectDto createDto)
        {
            if (_joinRepository.Get(id).FirstOrDefault(x => x.SideEffectId == createDto.SideEffectId) == null)
                return NotFound();
            if (_medicineRepository.Get(createDto.MedicineId) == null)
                return BadRequest("Medicine with the given ID doesn't exist.");
            if (_sideEffectRepository.Get(createDto.SideEffectId) == null)
                return BadRequest("SideEffect with the given ID doesn't exist.");


            if (_joinRepository.Get(createDto.MedicineId).FirstOrDefault(x => x.SideEffectId == createDto.SideEffectId) != null)
                return BadRequest("This SideEffect has already been added to this Medicine.");


            var sideEffect = new MedicineSideEffect()
            {
                Id = id,
                SideEffectId = createDto.SideEffectId,
                MedicineId = createDto.MedicineId,
            };

            if (_joinRepository.Update(sideEffect))
                return Ok(sideEffect);
            return BadRequest("MedicineSideEffect with the given ID already exists.");
        }

        // DELETE api/<MedicineSideEffectController>/5
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
