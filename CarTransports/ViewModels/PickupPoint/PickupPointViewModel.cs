using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarTransports.ViewModels
{
    public class PickupPointViewModel
    {
        public int PickupPointId { get; set; }
        public int CountryId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Περιγραφη")]
        public string Description { get; set; }

        [Display(Name = "Τ.Κ.")]
        public string Zip { get; set; }

        [Display(Name = "Πολη")]
        public string City { get; set; }

        [Display(Name = "Διευθυνση")]
        public string Address { get; set; }

        [Display(Name = "Ωραριο")]
        public string WorkingHours { get; set; }

        [Display(Name = "Γεωγραφικο πλατος")]
        public float Lat { get; set; }

        [Display(Name = "Γεωγραφικο μηκος")]
        public float Lng { get; set; }

        [DisplayName("Χωρα")]
        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}
