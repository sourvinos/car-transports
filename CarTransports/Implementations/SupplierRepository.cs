using CarTransports.Data;
using CarTransports.Interfaces;
using CarTransports.Models;

namespace CarTransports.Implementations
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(AppContext context) : base(context) { }
    }
}
