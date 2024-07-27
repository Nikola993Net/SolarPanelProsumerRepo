using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SolarPanelProsumer.Api.Model
{
    public class SolarPanel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Range(0, 3.0, ErrorMessage = "The value must be between 0 and 2.")]
        [Column(TypeName = "decimal(5, 2)")]
        /// <summary>
        /// This parameter will bi in the KW unit
        /// </summary>
        public decimal PowerPerHour { get; set; }
        /// <summary>
        /// This is the year of installation
        /// </summary>
        public int YearInstalled { get; set; }
        [Required]
        /// <summary>
        /// Id of prosumer where the solar panel is installed
        /// </summary>
        public int ProsumerID { get; set; }
        [ForeignKey("ProsumerID")]
        public virtual Prosumer? Prosumer { get; set; }
    }
}
