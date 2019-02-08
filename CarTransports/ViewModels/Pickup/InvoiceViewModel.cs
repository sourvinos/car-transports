using System.Collections.Generic;
using CarTransports.Models;

namespace CarTransports.ViewModels
{
    public class InvoiceViewModel
    {
        public string CustomerName { get; set; }
        public string Profession { get; set; }
        public string Address { get; set; }
        public string TaxNo { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }

        public List<string> Manufacturer { get; set; }
        public List<string> Model { get; set; }
        public List<string> VIN { get; set; }
        public List<string> PickupPointDescription { get; set; }
        public List<string> Price { get; set; }
    }
}

