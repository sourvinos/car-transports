using System.Collections.Generic;
using CarTransports.Models;

namespace CarTransports.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> GetAllWithPickups();
    }
}
