using Microsoft.AspNetCore.Mvc;
using Serilog;
using SolarPanelProsumer.Api.Interafaces;
using SolarPanelProsumer.Api.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SolarPanelProsumer.Api.Controllers
{
    [Route("api/Prosumer")]
    [ApiController]
    public class ProsumersController : ControllerBase
    {
        private readonly IProsumerRepository _prosumerRepository;

        public ProsumersController(IProsumerRepository prosumerRepository)
        {
            _prosumerRepository = prosumerRepository;
        }
        // GET: api/<ProsumersController>
        [HttpGet]
        public IActionResult GetProsumers()
        {
            return Ok(_prosumerRepository.GetAll());
        }

        // GET api/<ProsumersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_prosumerRepository.GetById(id));
        }

        // POST api/<ProsumersController>
        [HttpPost]
        public IActionResult PostProsumer([FromBody] Prosumer value)
        {
            _prosumerRepository.Add(value);
            return CreatedAtAction("GetProsumers", new { id = value.ID }, value);
        }

        // PUT api/<ProsumersController>/5
        [HttpPut("{id}")]
        public IActionResult PutProsumer(int id, [FromBody] Prosumer value)
        {
            _prosumerRepository.Update(value);
            return Ok(value);
        }

        // DELETE api/<ProsumersController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProsumer(int id)
        {
            _prosumerRepository.Delete(_prosumerRepository.GetById(id));
            return Ok();
        }

        //[Route("Updating")]
        [HttpPut("{prosumerID}/newSunnyHours")]
        public IActionResult UpdateSunnyHours( int prosumerID, [FromBody] float newSunnyHours)
        {
            Log.Information($"Update Prosumer: {prosumerID} set new sunny hours value: {newSunnyHours}");

            _prosumerRepository.UpdateSunnyHours(prosumerID, newSunnyHours);
            //_prosumerRepository.GetById(prosumerID);
            return Ok();
        }
    }
}
