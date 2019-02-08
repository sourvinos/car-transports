using CarTransports.Data;
using CarTransports.Interfaces;
using CarTransports.Models;

namespace CarTransports.Implementations
{
    public class PickupStateRepository : Repository<PickupState>, IPickupStateRepository
    {
        public PickupStateRepository(AppContext context) : base(context) { }
    }
}
