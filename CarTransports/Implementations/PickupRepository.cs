using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CarTransports.Data;
using CarTransports.Infrastructure;
using CarTransports.Interfaces;
using CarTransports.Models;

namespace CarTransports.Implementations
{
    public class PickupRepository : Repository<Pickup>, IPickupRepository
    {
        public PickupRepository(AppContext context) : base(context) { }

        public IEnumerable<Pickup> GetPickups(string pickupStateId = "", string debitStateId = "")
        {
            var criteria = "";

            criteria = CreateQuery(criteria, "PickupStateId", pickupStateId, "numeric");
            criteria = CreateQuery(criteria, "DebitStateId", debitStateId, "numeric");

            criteria = (criteria.Length > 0) ? criteria : "true";

            var pickups = context.Pickups
                .Include(i => i.Customer)
                .Include(i => i.PickupPoint)
                    .ThenInclude(i => i.Country)
                .Include(i => i.PickupState)
                .Include(i => i.DestinationPort)
                .Include(i => i.DebitState)
                .Include(i => i.Supplier)
                .Include(i => i.CurrentPosition)
                .Where(criteria)
                .ToList();

            return pickups;
        }

        public Pickup GetPickupById(int[] id)
        {
            var pickup = context.Pickups
                .Include(i => i.Customer)
                .Include(i => i.PickupPoint)
                    .ThenInclude(i => i.Country)
                .Where(i => i.PickupId == id[0]).SingleOrDefault();

            return pickup;
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
    }
}
