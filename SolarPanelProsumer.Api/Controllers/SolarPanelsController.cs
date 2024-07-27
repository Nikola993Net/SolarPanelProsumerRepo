using Microsoft.AspNetCore.Mvc;
using SolarPanelProsumer.Api.Interafaces;
using SolarPanelProsumer.Api.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SolarPanelProsumer.Api.Controllers
{
    [Route("api/solarpanels")]
    [ApiController]
    public class SolarPanelsController : ControllerBase
    {
        private readonly ISolarPanelRepository _repository;

        public SolarPanelsController(ISolarPanelRepository solarPanelRepository)
        {
            _repository = solarPanelRepository;
        }
        // GET: api/<SolarPanelsController>
        [HttpGet]
        public IEnumerable<SolarPanel> GetSolarPanels()
        {
            return _repository.GetAll();
        }

        // GET api/<SolarPanelsController>/5
        [HttpGet("{id}")]
        public string GetByID(int id)
        {
            return _repository.GetById(id).ToString();
        }

        // POST api/<SolarPanelsController>
        [HttpPost]
        public void PostSolarPanel([FromBody] SolarPanel value)
        {
            _repository.Add(value);
        }

        // PUT api/<SolarPanelsController>/5
        [HttpPut("{id}")]
        public void PutSolarPanel(int id, [FromBody] SolarPanel value)
        {
            _repository.Update(value);
        }

        // DELETE api/<SolarPanelsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(_repository.GetById(id));
        }
    }
}
