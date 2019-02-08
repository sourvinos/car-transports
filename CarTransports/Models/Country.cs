using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarTransports.Models
{
    public class Country
    {
        public int CountryId { get; set; }

        [Required]
        [StringLength (255)]
        [Display(Name = "Περιγραφη")]
        public string Description { get; set; }

        public ICollection<PickupPoint> PickupPoints { get; set; }
    }
}
