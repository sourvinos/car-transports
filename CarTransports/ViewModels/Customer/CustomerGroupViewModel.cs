using System.Collections.Generic;
using CarTransports.Models;

namespace CarTransports.ViewModels
{
    public class CustomerGroupViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerDescription { get; set; }
        public int PickupPerCustomerCount { get; set; }
        public int SumPerCustomer { get; set; }

        public IEnumerable<Pickup> PickupResults { get; set; }
    }
}
