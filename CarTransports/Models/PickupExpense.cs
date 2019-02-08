namespace CarTransports.Models
{
    public class PickupExpense
    {
        public int PickupExpenseId { get; set; }

        public int PickupId { get; set; }
        public int CollaboratorId { get; set; }
        public string Date { get; set; }
        public decimal Amount { get; set; }

        //public Collaborator Collaborator { get; set; }
    }
}
