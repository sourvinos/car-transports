using CarTransports.Data;
using CarTransports.Interfaces;
using CarTransports.Models;

namespace CarTransports.Implementations
{
    public class CurrentPositionRepository : Repository<CurrentPosition>, ICurrentPositionRepository
    {
        public CurrentPositionRepository(AppContext context) : base(context) { }
    }
}
