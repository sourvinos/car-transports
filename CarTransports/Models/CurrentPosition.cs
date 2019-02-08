using System.ComponentModel.DataAnnotations;

namespace CarTransports.Models
{
    public class CurrentPosition
    {
        public int CurrentPositionId { get; set; }

        [Required]
        [StringLength (255)]
        [Display(Name = "Περιγραφη")]
        public string Description { get; set; }
    }
}
