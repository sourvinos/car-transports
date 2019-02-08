using System.ComponentModel.DataAnnotations;

namespace CarTransports.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Περιγραφη")]
        public string RoleName { get; set; }
    }
}
