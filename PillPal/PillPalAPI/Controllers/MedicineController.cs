using Microsoft.AspNetCore.Mvc;
using PillPalAPI.Model;
using PillPalLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IDataStore _dataStore;
        public MedicineController(IDataStore dataStore) {
            _dataStore = dataStore;
        }
        // GET: api/<MedicineController>
        [HttpGet]
        public IEnumerable<Medicine> Get() => ((IItemStore<Medicine>)_dataStore).GetAll();

        // GET api/<MedicineController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var medicine = ((IItemStore<Medicine>)_dataStore).Get(id);
            if (medicine == null) 
                return NotFound();
            return Ok(medicine);
        }

        // POST api/<MedicineController>
        [HttpPost]
        public IActionResult Post([FromBody] Medicine medicine)
        {
            if (_dataStore.Update(medicine))
                return Ok(medicine);
            return BadRequest("Medicine with this ID already exists.");
        }

        // PUT api/<MedicineController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Medicine medicine)
        {
            if(_dataStore.Update(medicine))
                return Ok(medicine);
            return NotFound();
        }

        // DELETE api/<MedicineController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (((IItemStore<Medicine>)_dataStore).Delete(id))
                return NoContent();   
            return NotFound();
        }
    }
}
