using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using PillPalLib.DTOs.ReminderDTOs;
using PillPalAPI.Model;
using PillPalLib;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PillPalAPI.Controllers
{
    [Route("PillPal/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly IJoinStore<Reminder> _reminderRepository;
        private readonly IItemStore<Medicine> _medicineRepository;
        private readonly IItemStore<User> _userRepository;
        private readonly IValidator<CreateReminderDto> _validator;
        public ReminderController(
            IJoinStore<Reminder> reminderRepository, 
            IItemStore<User> userRepository, 
            IItemStore<Medicine> medicineRepository,
            IValidator<CreateReminderDto> validator)
        {
            _reminderRepository = reminderRepository;
            _userRepository = userRepository;
            _medicineRepository = medicineRepository;
            _validator = validator;
        }

        // GET: PillPal/Reminder
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll() => Ok(_reminderRepository.GetAll().ToList());

        // GET: PillPal/Reminder/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (HttpContext.User.Identity is not ClaimsIdentity identity)
                return BadRequest("Something went wrong.");
            var identitySid = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            if (identitySid != id.ToString())
            {
                var isAdmin = identity.Claims.Where(x => x.Type == ClaimTypes.Role && x.Value != null).Any(x => x.Value! == "Admin");
                if (!isAdmin)
                    return Forbid();
            }

            return Ok(_reminderRepository.Get(id));
        }

        // POST: PillPal/Reminder
        [HttpPost]
        public IActionResult Post([FromBody] CreateReminderDto reminderDto)
        {
            if (HttpContext.User.Identity is not ClaimsIdentity identity)
                return BadRequest("Something went wrong.");
            var identitySid = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            if (identitySid != reminderDto.UserId.ToString())
            {
                var isAdmin = identity.Claims.Where(x => x.Type == ClaimTypes.Role && x.Value != null).Any(x => x.Value! == "Admin");
                if (!isAdmin)
                    return Forbid();
            }

            var result = _validator.Validate(reminderDto);
            if (!result.IsValid)
                return BadRequest(result);

            var reminder = new Reminder()
            {
                UserId = reminderDto.UserId,
                MedicineId = reminderDto.MedicineId,
                DoseCount = reminderDto.DoseCount,
                TakingMethod = reminderDto.TakingMethod,
                When = TimeOnly.Parse(reminderDto.When),
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

        // PUT: PillPal/<Reminder/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateReminderDto reminderDto)
        {
            var pastReminder = _reminderRepository.GetAll().FirstOrDefault(x => x.Id == id);
            if (pastReminder == null)
                return NotFound();

            if (HttpContext.User.Identity is not ClaimsIdentity identity)
                return BadRequest("Something went wrong.");
            var identitySid = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            if (identitySid != reminderDto.UserId.ToString() || identitySid != pastReminder.UserId.ToString())
            {
                var isAdmin = identity.Claims.Where(x => x.Type == ClaimTypes.Role && x.Value != null).Any(x => x.Value! == "Admin");
                if (!isAdmin)
                    return Forbid();
            }

            var result = _validator.Validate(reminderDto);
            if (!result.IsValid)
                return BadRequest(result);

            var reminder = new Reminder()
            {
                UserId = reminderDto.UserId,
                MedicineId = reminderDto.MedicineId,
                DoseCount = reminderDto.DoseCount,
                TakingMethod = reminderDto.TakingMethod,
                When = TimeOnly.Parse(reminderDto.When),
                Id = id
            };
            if (_userRepository.Get(reminder.UserId) == null)
                return BadRequest("User with the given ID doesn't exist.");
            if (_medicineRepository.Get(reminder.MedicineId) == null)
                return BadRequest("Medicine with the given ID doesn't exist.");
            if (_reminderRepository.Update(reminder))
                return Ok(reminder);
            return BadRequest("Something went wrong.");
        }

        // DELETE: PillPal/Reminder/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var reminder = _reminderRepository.GetAll().FirstOrDefault(x => x.Id == id);
            if (reminder == null)
                return NotFound();

            if (HttpContext.User.Identity is not ClaimsIdentity identity)
                return BadRequest("Something went wrong.");
            var identitySid = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            if (identitySid != reminder.UserId.ToString())
            {
                var isAdmin = identity.Claims.Where(x => x.Type == ClaimTypes.Role && x.Value != null).Any(x => x.Value! == "Admin");
                if (!isAdmin)
                    return Forbid();
            }

            if (_reminderRepository.Delete(id))
                return NoContent();
            return BadRequest("Something went wrong.");
        }
    }
}
