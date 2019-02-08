using System.Collections.Generic;
using CarTransports.Models;

namespace CarTransports.ViewModels
{
    public class PickupPointIndexViewModel
    {
        public string CountryId { get; set; }
        public IEnumerable<CountryGroupViewModel> CountryGroupViewModel { get; set; }

        public IEnumerable<PickupPoint> PickupPointResults { get; set; }
    }
}
