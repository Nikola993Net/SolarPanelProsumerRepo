using Serilog;
using SolarPanelProsumer.Api.Interafaces;
using SolarPanelProsumer.Api.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<bool> AddProsumerRecordsAsync(ProsumerRecord record)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ProusmerRecordsSerivce");
                Log.Information($"Prosumer records clietn is created {client}");
                var dataAsString = JsonSerializer.Serialize(record);
                var content = new StringContent(dataAsString);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync($"api/prosumerRecords", content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex) 
            {
                Log.Error(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateProsumerRecordsAsync(ProsumerRecord record)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ProusmerRecordsSerivce");
                Log.Information($"Prosumer records clietn is created {client}");
                var dataAsString = JsonSerializer.Serialize(record);
                var content = new StringContent(dataAsString);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PutAsync($"api/prosumerRecords", content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteProsumerRecordsAsync(int prosumerId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ProusmerRecordsSerivce");
                Log.Information($"Prosumer records clietn is created {client}");
                var respone = await client.DeleteAsync($"api/prosumerRecords/{prosumerId}");
                if (respone.IsSuccessStatusCode) 
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            }
        }
    }
}
