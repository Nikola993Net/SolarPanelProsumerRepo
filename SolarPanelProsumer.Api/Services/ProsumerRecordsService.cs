using Serilog;
using SolarPanelProsumer.Api.Interafaces;
using SolarPanelProsumer.Api.Model;
using System.Text.Json;

namespace SolarPanelProsumer.Api.Services
{
    public class ProsumerRecordsService : IProsumerRecordsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IProsumerRecordsService> _logger;

        public ProsumerRecordsService(IHttpClientFactory httpClientFactory, ILogger<IProsumerRecordsService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;   
        }

        public async Task<(bool IsSuccess, ProsumerRecord ProsumerRecrod, string ErrorMessage)> GetProsumerRecordAsync(int prosumerId)
        {
            try
            {
                
                var client = _httpClientFactory.CreateClient("ProusmerRecordsSerivce");
                Log.Information($"Prosumer records clietn is created{client}");
                var respone = await client.GetAsync($"api/prosumerRecords/{prosumerId}");
                if (respone.IsSuccessStatusCode)
                {
                    Log.Information($"Prosumer records response is successfull {respone}");
                    var content = await respone.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<ProsumerRecord>(content, options);
                    return (true, result, null);
                }
                return (false, null, respone.ReasonPhrase);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<ProsumerRecord> ProsumerRecords, string ErrorMessage)> GetProsumerRecordsAsync()
        {
            try
            {

                var client = _httpClientFactory.CreateClient("ProusmerRecordsSerivce");
                Log.Information($"Prosumer records clietn is created {client}");
                var respone = await client.GetAsync($"api/prosumerRecords");
                
                if (respone.IsSuccessStatusCode)
                {
                    Log.Information($"Prosumer records response is successfull {respone}");
                    var content = await respone.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<ProsumerRecord>>(content, options);
                    return (true, result, null);
                }
                return (false, null, respone.ReasonPhrase);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return (false, null, ex.Message);
            }
        }
    }
}
