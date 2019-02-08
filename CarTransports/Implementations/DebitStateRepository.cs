using CarTransports.Data;
using CarTransports.Interfaces;
using CarTransports.Models;

namespace CarTransports.Implementations
{
    public class DebitStateRepository : Repository<DebitState>, IDebitStateRepository
    {
        public DebitStateRepository(AppContext context) : base(context) { }
    }
}
