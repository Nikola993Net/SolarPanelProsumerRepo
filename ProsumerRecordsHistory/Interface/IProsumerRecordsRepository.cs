using ProsumerRecordsHistory.Models;

namespace ProsumerRecordsHistory.Interface
{
    public interface IProsumerRecordsRepository
    {
        Task<(bool IsSuccess, IEnumerable<ProsumerRecord> Records, string ErrorMessage)> GetProsumerRecordsAsync();
        Task<(bool IsSuccess, ProsumerRecord Record, string ErrorMessage)> GetProsumerRecordAsynch(int Id);
    }
}
