using System.ComponentModel.DataAnnotations;

namespace CarTransports.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Χρηστης")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Κωδικος")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Να με θυμασαι")]
        public bool RememberMe { get; set; }
    }
}
