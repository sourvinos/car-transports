using System.ComponentModel.DataAnnotations;

namespace CarTransports.ViewModels
{
    public class UserIndexViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Ονομα")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Ρολος")]
        public string RoleName { get; set; }
    }
}
