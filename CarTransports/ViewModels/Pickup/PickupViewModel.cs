using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CarTransports.Models;

namespace CarTransports.ViewModels
{
    public class PickupViewModel
    {
        public int PickupId { get; set; }
        public int CustomerId { get; set; }
        public int PickupPointId { get; set; }
        public int PickupStateId { get; set; }
        public int DestinationPortId { get; set; }
        public int DebitStateId { get; set; }
        public int SupplierId { get; set; }
        public int CurrentPositionId { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Κωδ. αυτοκινητου")]
        public string CarNo { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Κατασκευαστης")]
        public string Manufacturer { get; set; }

        [StringLength(255)]
        [DisplayName("Μοντελο")]
        public string Model { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Κωδ. παραλαβης")]
        public string PickupNo { get; set; }

        [Required]
        [Range(0.00, 99999.99)]
        [DisplayName("Χρεωση")]
        public int Price { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Αρ. πλαισιου")]
        public string VIN { get; set; }

        [DisplayName("Πελατης")]
        public IEnumerable<SelectListItem> Customers { get; set; }
        [DisplayName("Σημειο παραλαβης")]
        public IEnumerable<SelectListItem> PickupPoints { get; set; }
        [DisplayName("Σταδιο παραλαβης")]
        public IEnumerable<SelectListItem> PickupStates { get; set; }
        [DisplayName("Λιμανι προορισμου")]
        public IEnumerable<SelectListItem> DestinationPorts { get; set; }
        [DisplayName("Σταδιο τιμολογησης")]
        public IEnumerable<SelectListItem> DebitStates { get; set; }
        [DisplayName("Εμπορος")]
        public IEnumerable<SelectListItem> Suppliers { get; set; }
        [DisplayName("Τρεχουσα θεση")]
        public IEnumerable<SelectListItem> CurrentPositions { get; set; }

        public IList<Collaborator> Collaborators { get; set; }
    }
}
