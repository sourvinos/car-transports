using System.Collections.Generic;

namespace CarTransports.Models
{
    public class PickupPoint
    {
        public int PickupPointId { get; set; }

        public string Description { get; set; }
        public int CountryId { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string WorkingHours { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }

        public string FullAddress
        {
            get
            {
                return Zip + " - " + Description;
            }
        }

        public Country Country { get; set; }
        public ICollection<Pickup> Pickups { get; set; }
    }
}
