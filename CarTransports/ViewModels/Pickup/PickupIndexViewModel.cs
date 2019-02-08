using System.Collections.Generic;
using CarTransports.Models;

namespace CarTransports.ViewModels
{
    public class PickupIndexViewModel
    {
        public string PickupStateId { get; set; }
        public IEnumerable<PickupStateGroupViewModel> PickupStateGroupViewModel { get; set; }

        public string DebitStateId { get; set; }
        public IEnumerable<DebitStateGroupViewModel> DebitStateGroupViewModel { get; set; }

        public IEnumerable<Pickup> PickupResults { get; set; }
    }
}
