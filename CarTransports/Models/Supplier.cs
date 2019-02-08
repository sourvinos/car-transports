using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarTransports.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        [Required]
        [StringLength (255)]
        [Display(Name = "Περιγραφη")]
        public string Description { get; set; }

        public ICollection<Pickup> Pickups { get; set; }
    }
}
