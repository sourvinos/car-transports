using System.Collections.Generic;
using CarTransports.Models;

namespace CarTransports.Interfaces
{
    public interface ICollaboratorRepository : IRepository<Collaborator>
    {
        IEnumerable<Collaborator> GetAllWithExpenses();
        IEnumerable<Collaborator> GetAllWithExpensesById(int id);
    }
}
