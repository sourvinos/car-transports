using System.ComponentModel.DataAnnotations;

namespace CarTransports.ViewModels
{
    public class RoleIndexViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Περιγραφη")]
        public string RoleName { get; set; }
    }
}
