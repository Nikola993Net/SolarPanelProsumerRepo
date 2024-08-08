using Microsoft.AspNetCore.Mvc;
using ProsumerRecordsHistory.Interface;

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
    }
}
