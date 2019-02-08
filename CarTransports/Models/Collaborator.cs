using System.ComponentModel.DataAnnotations;

namespace CarTransports.Models
{
    public class Collaborator
    {
        public int CollaboratorId { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Υποχρεωτικό πεδίο")]
        [Display(Name = "Επωνυμια")]
        public string Description { get; set; }

        public PickupExpense PickupExpense { get; set; }
    }
}
