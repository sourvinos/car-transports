using System.Collections.Generic;

namespace CarTransports.Models
{
    public class Pickup
    {
        public int PickupId { get; set; }

        public int CustomerId { get; set; }
        public int PickupPointId { get; set; }
        public int PickupStateId { get; set; }
        public int DestinationPortId { get; set; }
        public int DebitStateId { get; set; }
        public int SupplierId { get; set; }
        public int CurrentPositionId { get; set; }

        public string CarNo { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string PickupNo { get; set; }
        public string VIN { get; set; }

        public int Price { get; set; }

        public Customer Customer { get; set; }
        public PickupPoint PickupPoint { get; set; }
        public PickupState PickupState { get; set; }
        public Port DestinationPort { get; set; }
        public DebitState DebitState { get; set; }
        public Supplier Supplier { get; set; }
        public CurrentPosition CurrentPosition { get; set; }

        public IEnumerable<PickupExpense> PickupExpenses { get; set; }
    }
}
