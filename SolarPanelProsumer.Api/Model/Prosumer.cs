using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SolarPanelProsumer.Api.Model
{
    public class Prosumer
    {
        [Key]
        public int ID { get; set; }
        [Required]
        /// <summary>
        /// Name of prosumer
        /// </summary>
        public string? Name { get; set; }
        [Required]
        /// <summary>
        /// Surname of prosumer
        /// </summary>
        public string? Surname { get; set; }
        [Required]
        [Range(0, 100.0, ErrorMessage = "The value must be between 0 and 100.")]
        [Column(TypeName = "decimal(5, 2)")]
        /// <summary>
        /// Average power which the prosumer usage in whole day
        /// </summary>
        public decimal DalyPowerConsuption { get; set; }
        [Required]
        [Range(0, 14.0, ErrorMessage = "The value must be between 0 and 14.")]
        [Column(TypeName = "decimal(5, 1)")]
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
