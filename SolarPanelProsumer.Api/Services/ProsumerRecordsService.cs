using RecordsHistory.Grpc;
using Serilog;
using SolarPanelProsumer.Api.Interafaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SolarPanelProsumer.Api.Services
{
    public class ProsumerRecordsService : IProsumerRecordsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IProsumerRecordsService> _logger;
        private readonly ProsumerRecords.ProsumerRecordsClient _prosumerRecordsClient;

        public ProsumerRecordsService(IHttpClientFactory httpClientFactory, ILogger<IProsumerRecordsService> logger, ProsumerRecords.ProsumerRecordsClient prosumerRecordsClient)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;   
            _prosumerRecordsClient = prosumerRecordsClient;
        }

        public async Task<(bool IsSuccess, Model.ProsumerRecord ProsumerRecrod, string ErrorMessage)> GetProsumerRecordAsync(int prosumerId)
        {
            //try
            //{

            //    var client = _httpClientFactory.CreateClient("ProusmerRecordsSerivce");
            //    Log.Information($"Prosumer records clietn is created{client}");
            //    var respone = await client.GetAsync($"api/prosumerRecords/{prosumerId}");
            //    if (respone.IsSuccessStatusCode)
            //    {
            //        Log.Information($"Prosumer records response is successfull {respone}");
            //        var content = await respone.Content.ReadAsStringAsync();
            //        var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            //        var result = JsonSerializer.Deserialize<Model.ProsumerRecord>(content, options);
            //        return (true, result, null);
            //    }
            //    return (false, null, respone.ReasonPhrase);
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex.Message);
            //    return (false, null, ex.Message);
            //}

            try
            {
                
                GetProsumerRecrodByIdRequest requet = new GetProsumerRecrodByIdRequest { RecordId = prosumerId };
                GetProsumerRecordByIdResponse response = await _prosumerRecordsClient.GetProsumerRecordAsync(requet);
                Log.Information($"Prosumer records gRPC respond : {response}");
                if (response != null)
                {
                    var prosumerRecord = new Model.ProsumerRecord
                    {
                        ID = response.Record.ID,
                        PowerGenerated = (decimal)response.Record.PowerGenerated,
                        PowerConsumped = (decimal)response.Record.PowerConsumped,
                        PowerPulled = (decimal)response.Record.PowerPulled,
                        Day = response.Record.Day.ToDateTime()
                    };
                    Log.Information($"Prosumer records respond mapped gRPC: {response}");
                    return (true, prosumerRecord, null);
                }
                Log.Information($"Prosumer records respond is empty");
                return (false, null, "the respons is empty");
            }
            catch (Exception ex) 
            {
                Log.Error(ex.Message);
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Model.ProsumerRecord> ProsumerRecords, string ErrorMessage)> GetProsumerRecordsAsync()
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
                    var result = JsonSerializer.Deserialize<IEnumerable<Model.ProsumerRecord>>(content, options);
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

        public async Task<bool> AddProsumerRecordsAsync(Model.ProsumerRecord record)
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

        public async Task<bool> UpdateProsumerRecordsAsync(Model.ProsumerRecord record)
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
