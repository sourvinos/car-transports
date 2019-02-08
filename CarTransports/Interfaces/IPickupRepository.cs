using System.Collections.Generic;
using CarTransports.Models;

namespace CarTransports.Interfaces
{
    public interface IPickupRepository : IRepository<Pickup>
    {
        Pickup GetPickupById(int[] id);

        IEnumerable<Pickup> GetPickups(string pickupStateId, string debitStateId);
    }
}
