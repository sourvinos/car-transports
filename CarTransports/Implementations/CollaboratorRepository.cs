using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CarTransports.Data;
using CarTransports.Interfaces;
using CarTransports.Models;

namespace CarTransports.Implementations
{
    public class CollaboratorRepository : Repository<Collaborator>, ICollaboratorRepository
    {
        public CollaboratorRepository(AppContext context) : base(context) { }

        public IEnumerable<Collaborator> GetAllWithExpenses()
        {
            return context.Collaborators.Include(i => i.PickupExpense);
        }

        public IEnumerable<Collaborator> GetAllWithExpensesById(int id)
        {
            return context.Collaborators.Include(i => i.PickupExpense).Where(i => i.PickupExpense.PickupId == id);
        }
    }
}
