using Microsoft.EntityFrameworkCore;

namespace ProsumerRecordsHistory.Db
{
    public class ProsumerRecordsDbContext :DbContext
    {
        public DbSet<ProsumerRecordDbModel> ProsumerRecords { get; set; }
        public ProsumerRecordsDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
