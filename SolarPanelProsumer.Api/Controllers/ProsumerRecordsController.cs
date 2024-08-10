using Microsoft.AspNetCore.Mvc;
using SolarPanelProsumer.Api.Interafaces;
using SolarPanelProsumer.Api.Model;

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

        [HttpPost]
        public async Task<IActionResult> PostProsumerRecord([FromBody] ProsumerRecord record)
        {
            var result = await _prosumerRecordSerivce.AddProsumerRecordsAsync(record);
            if (result)
                return Ok(result);
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> PutProsumerRecord([FromBody] ProsumerRecord record)
        {
            var result = await _prosumerRecordSerivce.UpdateProsumerRecordsAsync(record);
            if (result)
                return Ok(result);
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProsumerRecord(int id)
        {
            var result = await _prosumerRecordSerivce.DeleteProsumerRecordsAsync(id);
            if (result)
                return Ok();
            return NotFound();
        }
    }
}
