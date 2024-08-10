using Microsoft.AspNetCore.Mvc;
using ProsumerRecordsHistory.Interface;
using ProsumerRecordsHistory.Models;

namespace ProsumerRecordsHistory.Controllers
{
    [ApiController]
    [Route("api/prosumerRecords")]
    public class ProsumerRecordsCotnroller : ControllerBase
    {
        private readonly IProsumerRecordsRepository _provider;
        public ProsumerRecordsCotnroller(IProsumerRecordsRepository provider)
        {
            _provider = provider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProsumerRecordsAsynch()
        {
            var result = await _provider.GetProsumerRecordsAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Records);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProsumerRecordAsync(int id)
        {
            var result = await _provider.GetProsumerRecordAsynch(id);
            if (result.IsSuccess)
            {
                return Ok(result.Record);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostProsumerRecordAsync([FromBody] ProsumerRecord record)
        {
            var isAdded = await _provider.AddProsumerRecordAsync(record);
            if (isAdded) 
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> PutProsumerRecordAsyc([FromBody] ProsumerRecord record)
        {
            var isUpdated = await _provider.UpdateProsumerRecordAsyc(record);
            if (isUpdated)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProsumerRecordAsync(int id)
        {
            var record = await _provider.GetProsumerRecordAsynch(id);
            if (!record.IsSuccess)
            {
                return NotFound();
            }

            if (await _provider.DeleteProsumerRecordAsync(record.Record))
            {
                return Ok();
            }
            return StatusCode(500);
        }
    }
}
