using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarTransports.Models
{
    public class Transporter
    {
        public int TransporterId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Επωνυμια")]
        public string Description { get; set; }

        public ICollection<Pickup> Pickups { get; set; }
    }
}
