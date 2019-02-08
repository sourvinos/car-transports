using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarTransports.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Ονομα χρηστη")]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Κωδικος")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Επιβεβαιωση κωδικου")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Πληρες ονομα")]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Ρολος")]
        public string AppRoleId { get; set; }

        public List<SelectListItem> AppRoles { get; set; }
    }
}
