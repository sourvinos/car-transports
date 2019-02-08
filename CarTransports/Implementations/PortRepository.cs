using CarTransports.Data;
using CarTransports.Interfaces;
using CarTransports.Models;

namespace CarTransports.Implementations
{
    public class PortRepository : Repository<Port>, IPortRepository
    {
        public PortRepository(AppContext context) : base(context) { }
    }
}
