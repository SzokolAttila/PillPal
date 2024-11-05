using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PillPalAPI.Model;
using PillPalLib;

namespace PillPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly IItemStore<Reminder> _reminderRepository;
        public ReminderController(IItemStore<Reminder> reminderRepository)
        {
            _reminderRepository = reminderRepository;
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
        public IActionResult Post([FromBody] Reminder reminder)
        {
            if (_reminderRepository.Add(reminder))
            {
                return Ok(reminder);
            }
            return BadRequest("Reminder with this ID already exists.");
        }

        // PUT: api/<ReminderController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Reminder reminder)
        {
            if (_reminderRepository.Update(reminder))
            {
                return Ok(reminder);
            }
            return BadRequest();
        }

        // DELETE: api/<ReminderController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_reminderRepository.Delete(id))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
