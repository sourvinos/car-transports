using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CarTransports.Data;
using CarTransports.Interfaces;
using CarTransports.Models;

namespace CarTransports.Implementations
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppContext context) : base(context) { }

        public IEnumerable<Customer> GetAllWithPickups()
        {
            var customers = context.Customers.OrderBy(o => o.GreekCompanyName)
                .Include(i => i.Pickups)
                    .ThenInclude(i => i.PickupPoint)
                        .ThenInclude(i => i.Country)
                .Include(i => i.Pickups)
                    .ThenInclude(i => i.PickupState);

            return customers.ToList();
        }
    }
}
