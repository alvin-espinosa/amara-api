using Data.TestEntity;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class RentingContext : DbContext
    {
        public RentingContext(DbContextOptions<RentingContext> opt) : base(opt) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var customers = new List<Customer>
            //{
            //    new Customer { Id = Guid.NewGuid(), Name = "Alvin", Address = "Karuhatan" },
            //    new Customer { Id = Guid.NewGuid(), Name = "Amara", Address = "Valenzuela" },
            //    new Customer { Id = Guid.NewGuid(), Name = "Kresleen", Address = "Davao" },
            //    new Customer { Id = Guid.NewGuid(), Name = "Andrei", Address = "Manila" },
            //};

            //modelBuilder.Entity<Customer>().HasData(customers);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
