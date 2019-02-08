using System.Linq;
using CarTransports.Models;

namespace CarTransports.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Pickups.Any())
            {
                return;
            }

            // Sample
            var countries = new Country[]
            {
                new Country { Description = "Austria" },
                new Country { Description = "Belgium" },
                new Country { Description = "Czechia" },
                new Country { Description = "France" },
                new Country { Description = "Germany" },
                new Country { Description = "Italy" },
                new Country { Description = "Netherlands" }
            };

            foreach (Country s in countries)
            {
                context.Countries.Add(s);
            }

            context.SaveChanges();
        }
    }
}
