using Microsoft.AspNetCore.Mvc;
using SolarPanelProsumer.Api.Interafaces;

namespace SolarPanelProsumer.Api.Controllers
{
    [ApiController]
    [Route("api/records")]
    public class ProsumerRecordsController : ControllerBase
    {
        private readonly IProsumerRecordsService _prosumerRecordSerivce;
        public ProsumerRecordsController(IProsumerRecordsService prosumerRecrodSerivce)
        {
            _prosumerRecordSerivce = prosumerRecrodSerivce;
        }

        [HttpGet]
        public async Task<IActionResult> GetProsumerRecords()
        {
            var result = await _prosumerRecordSerivce.GetProsumerRecordsAsync();
            if (result.IsSuccess)
                return Ok(result.ProsumerRecords);
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProsumerRecord(int id)
        {
            var result = await _prosumerRecordSerivce.GetProsumerRecordAsync(id);
            if (result.IsSuccess)
                return Ok(result.ProsumerRecrod);
            return NotFound();
        }
    }
}
