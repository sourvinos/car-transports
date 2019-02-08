using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarTransports.Identity;
using CarTransports.Models;

namespace CarTransports.Data
{
    public class AppContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }

        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CurrentPosition> CurrentPositions { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DebitState> DebitStates { get; set; }
        public DbSet<Pickup> Pickups { get; set; }
        public DbSet<PickupExpense> PickupExpenses { get; set; }
        public DbSet<PickupPoint> PickupPoints { get; set; }
        public DbSet<PickupState> PickupStates { get; set; }
        public DbSet<Port> Ports { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
