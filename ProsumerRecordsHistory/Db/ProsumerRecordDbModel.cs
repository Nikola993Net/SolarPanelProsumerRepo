using System.ComponentModel.DataAnnotations;

namespace ProsumerRecordsHistory.Db
{
    public class ProsumerRecordDbModel
    {
        [Key]
        public int ID { get; set; }
        public decimal PowerGenerated { get; set; }
        public decimal PowerConsumped { get; set; }
        public decimal PowerPulled { get; set; }
        public DateTime Day { get; set; }
    }
}
