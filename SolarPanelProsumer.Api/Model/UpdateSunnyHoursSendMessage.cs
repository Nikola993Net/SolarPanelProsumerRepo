namespace SolarPanelProsumer.Api.Model
{
    /// <summary>
    /// the class will be use for creating model which will send to the SolarPanelProsumer App through the RabbitMQ protocol 
    /// </summary>
    public class UpdateSunnyHoursSendMessage
    {
        /// <summary>
        /// Prosumer - represents the prosumer where the sunny hours will be updated
        /// </summary>
        public Prosumer Prosumer { get; set; }
        /// <summary>
        /// The value which should be set in the prosumer for the sunny hour
        /// </summary>
        public decimal NewSunnyHours { get; set; }
    }
}
