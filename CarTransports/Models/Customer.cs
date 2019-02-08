using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarTransports.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Επωνυμια")]
        public string GreekCompanyName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Επωνυμια τιμολογησης")]
        public string EnglishCompanyName { get; set; }

        [StringLength(255)]
        [Display(Name = "Διευθυνση")]
        public string Address { get; set; }

        [StringLength(255)]
        [Display(Name = "Πολη")]
        public string City { get; set; }

        [StringLength(255)]
        [Display(Name = "Τ.Κ.")]
        public string Zip { get; set; }

        [StringLength(255)]
        [Display(Name = "Εκτελωνιστης")]
        public string CustomsClearer { get; set; }

        [StringLength(255)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(255)]
        [Display(Name = "Τηλεφωνο 1")]
        public string Phone { get; set; }

        [StringLength(255)]
        [Display(Name = "Τηλεφωνο 2")]
        public string PhoneSecondary { get; set; }

        [StringLength(255)]
        [Display(Name = "Δραστηριοτητα")]
        public string Profession { get; set; }

        [StringLength(255)]
        [Display(Name = "Προσφωνηση")]
        public string Salutation { get; set; }

        [StringLength(255)]
        [Display(Name = "Α.Φ.Μ.")]
        public string TaxNo { get; set; }

        [StringLength(255)]
        [Display(Name = "Γενικη ονοματος")]
        public string GenitiveCase { get; set; }

        [StringLength(255)]
        [Display(Name = "Υπευθυνος επικοινωνιας")]
        public string ContactPerson { get; set; }

        public ICollection<Pickup> Pickups { get; set; }
    }
}
