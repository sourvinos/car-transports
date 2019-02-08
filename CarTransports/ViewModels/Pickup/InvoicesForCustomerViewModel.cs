using System.Collections.Generic;
using CarTransports.Models;

namespace CarTransports.ViewModels
{
    public class InvoicesForCustomerViewModel
    {
        public Customer Customer { get; set; }
        public List<Pickup> Pickups { get; set; }

        public string[] PickupIds { get; set; }

        public InvoicesForCustomerViewModel()
        {
            Customer = new Customer();
            Pickups = new List<Pickup>();
        }

    }
}
