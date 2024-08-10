using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProsumerRecordsHistory.Db;
using ProsumerRecordsHistory.Interface;
using ProsumerRecordsHistory.Models;

namespace ProsumerRecordsHistory.Repository
{
    public class ProsumerRecordsRepository : IProsumerRecordsRepository
    {
        private readonly ProsumerRecordsDbContext _context;
        private readonly ILogger<IProsumerRecordsRepository> _logger;
        private readonly IMapper _mapper;

        public ProsumerRecordsRepository(ProsumerRecordsDbContext context, ILogger<IProsumerRecordsRepository> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;   
            _mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!_context.ProsumerRecords.Any())
            {
                _context.ProsumerRecords.Add(new Db.ProsumerRecordDbModel() { ID = 1, Day = DateTime.Now.Date, PowerGenerated = (decimal)5.00, PowerConsumped = (decimal)11.0, PowerPulled = (decimal)6.00 });
                _context.ProsumerRecords.Add(new Db.ProsumerRecordDbModel() { ID = 2, Day = DateTime.Now.Date, PowerGenerated = (decimal)6.00, PowerConsumped = (decimal)11.0, PowerPulled = (decimal)5.00 });
                _context.ProsumerRecords.Add(new Db.ProsumerRecordDbModel() { ID = 3, Day = DateTime.Now.Date, PowerGenerated = (decimal)3.00, PowerConsumped = (decimal)11.0, PowerPulled = (decimal)8.00 });
                _context.ProsumerRecords.Add(new Db.ProsumerRecordDbModel() { ID = 4, Day = DateTime.Now.Date, PowerGenerated = (decimal)5.00, PowerConsumped = (decimal)11.0, PowerPulled = (decimal)6.00 });
                _context.SaveChanges();
            }
        }
        public async Task<(bool IsSuccess, ProsumerRecord Record, string ErrorMessage)> GetProsumerRecordAsynch(int Id)
        {
            try
            {
                _logger?.LogInformation("Quering prosumer record");
                var prosuerRecord = await _context.ProsumerRecords.FirstOrDefaultAsync(x => x.ID == Id);
                if (prosuerRecord != null)
                {
                    _logger.LogInformation("Record is found");
                    var result = _mapper.Map<Db.ProsumerRecordDbModel, Models.ProsumerRecord>(prosuerRecord);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<ProsumerRecord> Records, string ErrorMessage)> GetProsumerRecordsAsync()
        {
            try
            {
                _logger?.LogInformation("Quering prosumer records");
                var prosuerRecords = await _context.ProsumerRecords.ToListAsync();
                if (prosuerRecords != null && prosuerRecords.Any())
                {
                    _logger?.LogInformation($"{prosuerRecords.Count} record(s) found");
                    var result = _mapper.Map<IEnumerable<Db.ProsumerRecordDbModel>, IEnumerable<Models.ProsumerRecord>>(prosuerRecords);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<bool> AddProsumerRecordAsync(ProsumerRecord prosumerRecod)
        {
            try
            {
                _logger?.LogInformation("Adding the prosumer record");
                var record = _mapper.Map<ProsumerRecord, ProsumerRecordDbModel>(prosumerRecod);
                if (record != null)
                {
                    _context.ProsumerRecords.Add(record);
                    return (await _context.SaveChangesAsync() > 0);
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return false;
            }
        }

        public async Task<bool> UpdateProsumerRecordAsyc(ProsumerRecord record)
        {
            try
            {
                _logger?.LogInformation("Updating the prosumer record");
                var newRecord = _mapper.Map<ProsumerRecord, ProsumerRecordDbModel>(record);
                var oldProsumer = _context.ProsumerRecords.FirstOrDefault(x => x.ID == newRecord.ID);
                if (oldProsumer != null)
                {
                    oldProsumer.PowerPulled = newRecord.PowerPulled;
                    oldProsumer.PowerGenerated = newRecord.PowerGenerated;
                    oldProsumer.PowerConsumped = newRecord.PowerConsumped;
                    oldProsumer.Day = newRecord.Day;
                    return (await _context.SaveChangesAsync() > 0);
                }
                return false;
            }
            catch (Exception ex) 
            {
                _logger?.LogError(ex.ToString());
                return false;
            }
        }

        public async Task<bool> DeleteProsumerRecordAsync(ProsumerRecord record)
        {
            try
            {
                //var newRecord = _mapper.Map<ProsumerRecord, ProsumerRecordDbModel>(record);
                var prosuerRecord = await _context.ProsumerRecords.FirstOrDefaultAsync(x => x.ID == record.ID);
                _logger?.LogInformation("Deleting the prosumer record");
                _context.ProsumerRecords.Remove(prosuerRecord);
                return await _context.SaveChangesAsync() > 0;

            }
            catch (Exception ex) 
            {
                _logger?.LogError(ex.ToString());
                return false;
            }
        }
    }
}
