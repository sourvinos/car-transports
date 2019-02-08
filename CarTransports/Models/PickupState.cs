using System.ComponentModel.DataAnnotations;

namespace CarTransports.Models
{
    public class PickupState
    {
        public int PickupStateId { get; set; }

        [Required]
        [StringLength (255)]
        [Display(Name = "Περιγραφη")]
        public string Description { get; set; }
    }
}
