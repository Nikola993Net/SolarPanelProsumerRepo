namespace SolarPanelProsumer.Api.Model
{
    public class ProsumerRecord
    {
        public int ID { get; set; }
        public decimal PowerGenerated { get; set; }
        public decimal PowerConsumped { get; set; }
        public decimal PowerPulled { get; set; }
        public DateTime Day { get; set; }
    }
}
