using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CarTransports.Data;
using CarTransports.Infrastructure;
using CarTransports.Interfaces;
using CarTransports.Models;

namespace CarTransports.Implementations
{
    public class PickupPointRepository : Repository<PickupPoint>, IPickupPointRepository
    {
        public PickupPointRepository(AppContext context) : base(context) { }

        public IEnumerable<PickupPoint> GetPickupPoints(string countryId = "")
        {
            var criteria = "";

            criteria = CreateQuery(criteria, "CountryId", countryId, "numeric");

            criteria = (criteria.Length > 0) ? criteria : "true";

            var pickupPoints = context.PickupPoints
                .Include(i => i.Country)
                .Where(criteria)
                .ToList();

            return pickupPoints;
        }

        private string CreateQuery(string condition, string fieldName, string query, string fieldType, string comparison = "=", string queryWildCard = "")
        {
            if (query != null && query.Length != 0)
            {
                string[] parts = query.Split('-');
                condition += (condition.Length > 0) ? " and (" : "(";
                string doubleQuotes = "";
                doubleQuotes = (fieldType == "string") ? "\"" : "";

                foreach (string id in parts)
                {
                    condition += fieldName + " " + comparison + "" + queryWildCard + doubleQuotes + id + doubleQuotes + queryWildCard + " or ";
                }

                condition = condition.Substring(0, condition.Length - 4) + ")";
            }

            return condition;
        }

        public IEnumerable<PickupPoint> GetAllWithCountries()
        {
            return context.PickupPoints.OrderBy(o => o.Country.Description).ThenBy(o => o.FullAddress).Include(i => i.Country);
        }
    }
}
