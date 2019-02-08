using System.Collections.Generic;
using CarTransports.Models;

namespace CarTransports.Interfaces
{
    public interface IPickupPointRepository : IRepository<PickupPoint>
    {
        IEnumerable<PickupPoint> GetAllWithCountries();
        IEnumerable<PickupPoint> GetPickupPoints(string countryId);
    }
}
