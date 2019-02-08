using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarTransports.Models
{
    public class Port
    {
        public int PortId { get; set; }

        [Required]
        [StringLength (255)]
        [Display(Name = "Περιγραφη")]
        public string Description { get; set; }

        public ICollection<Pickup> Pickups { get; set; }
    }
}
