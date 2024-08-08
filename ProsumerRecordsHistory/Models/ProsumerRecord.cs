using System.ComponentModel.DataAnnotations;

namespace ProsumerRecordsHistory.Models
{
    public class ProsumerRecord
    {
        [Key]
        public int ID { get; set; }
        public decimal PowerGenerated { get; set; }
        public decimal PowerConsumped { get; set; }
        public decimal PowerPulled { get; set; }
        public DateTime Day { get; set; }
    }
}
