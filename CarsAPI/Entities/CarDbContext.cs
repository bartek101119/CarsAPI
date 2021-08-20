using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPI.Entities
{
    public class CarDbContext : DbContext
    {
        private string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CarsDb;Trusted_Connection=True;";
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarCompany> CarCompanies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>();
            modelBuilder.Entity<CarCompany>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
