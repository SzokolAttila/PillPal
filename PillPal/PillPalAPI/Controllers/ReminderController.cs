using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using PillPalLib.DTOs.ReminderDTOs;
using PillPalAPI.Model;
using PillPalLib;
using FluentValidation;

namespace PillPalAPI.Controllers
{
    [Route("PillPal/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly IItemStore<Reminder> _reminderRepository;
        private readonly IItemStore<Medicine> _medicineRepository;
        private readonly IItemStore<User> _userRepository;
        private readonly IValidator<CreateReminderDto> _validator;
        public ReminderController(
            IItemStore<Reminder> reminderRepository, 
            IItemStore<User> userRepository, 
            IItemStore<Medicine> medicineRepository,
            IValidator<CreateReminderDto> validator)
        {
            _reminderRepository = reminderRepository;
            _userRepository = userRepository;
            _medicineRepository = medicineRepository;
            _validator = validator;
        }

        // GET: api/<ReminderController>
        [HttpGet]
        public IActionResult GetAll() => Ok(_reminderRepository.GetAll().ToList());

        // GET: api/<ReminderController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _reminderRepository.Get(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/<ReminderController>/Create
        [HttpPost]
        public IActionResult Post([FromBody] CreateReminderDto reminderDto)
        {
            var result = _validator.Validate(reminderDto);
            if (!result.IsValid)
                return BadRequest(result);

            var reminder = new Reminder()
            {
                UserId = reminderDto.UserId,
                MedicineId = reminderDto.MedicineId,
                DoseCount = reminderDto.DoseCount,
                DoseMg = reminderDto.DoseMg,
                TakingMethod = reminderDto.TakingMethod,
                When = reminderDto.When,
            };
            if (_userRepository.Get(reminder.UserId) == null)
                return BadRequest("User with the given ID doesn't exist.");
            if (_medicineRepository.Get(reminder.MedicineId) == null)
                return BadRequest("Medicine with the given ID doesn't exist."); 
            if (_reminderRepository.Add(reminder))
            {
                return Ok(reminder);
            }
            return BadRequest("Reminder with this ID already exists.");
        }

        // PUT: api/<ReminderController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateReminderDto reminderDto)
        {
            var result = _validator.Validate(reminderDto);
            if (!result.IsValid)
                return BadRequest(result);

            var reminder = new Reminder()
            {
                UserId = reminderDto.UserId,
                MedicineId = reminderDto.MedicineId,
                DoseCount = reminderDto.DoseCount,
                DoseMg = reminderDto.DoseMg,
                TakingMethod = reminderDto.TakingMethod,
                When = reminderDto.When,
                Id = id
            };
            if (_userRepository.Get(reminder.UserId) == null)
                return BadRequest("User with the given ID doesn't exist.");
            if (_medicineRepository.Get(reminder.MedicineId) == null)
                return BadRequest("Medicine with the given ID doesn't exist.");
            if (_reminderRepository.Update(reminder))
            {
                return Ok(reminder);
            }
            return NotFound();
        }

        // DELETE: api/<ReminderController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_reminderRepository.Delete(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
