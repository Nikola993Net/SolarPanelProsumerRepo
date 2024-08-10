using SolarPanelProsumer.Api.Model;

namespace SolarPanelProsumer.Api.Interafaces
{
    public interface IProsumerRecordsService
    {
        Task<(bool IsSuccess, ProsumerRecord ProsumerRecrod, string ErrorMessage)> GetProsumerRecordAsync(int prosumerId);
        Task<(bool IsSuccess, IEnumerable<ProsumerRecord> ProsumerRecords, string ErrorMessage)> GetProsumerRecordsAsync();

        Task<bool> AddProsumerRecordsAsync(ProsumerRecord record);

        Task<bool> UpdateProsumerRecordsAsync(ProsumerRecord record);
        Task<bool> DeleteProsumerRecordsAsync(int prosumerId);
    }
}
