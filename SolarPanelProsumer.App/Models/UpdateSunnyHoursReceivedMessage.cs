using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarPanelProsumer.App.Models
{
    public class UpdateSunnyHoursReceivedMessage
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

    public class Prosumer
    {
        public int ID { get; set; }
        /// <summary>
        /// Name of prosumer
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Surname of prosumer
        /// </summary>
        public string? Surname { get; set; }

        /// <summary>
        /// Average power which the prosumer usage in whole day
        /// </summary>
        public decimal DalyPowerConsuption { get; set; }

        /// <summary>
        /// Average Sunny hour per day
        /// </summary>
        public decimal SunnyHourPerDay { get; set; }

        /// <summary>
        /// This is the power which consumer generate from their solar panel
        /// </summary>
        public decimal GeneratePower { get; set; }

        /// <summary>
        /// The power which consumer pulled form the distribution network per day
        /// </summary>
        public decimal PulledPower { get; set; }
    }
}
