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
        public DbSet<CarCompany> CarCompany { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>();
            modelBuilder.Entity<CarCompany>();
            modelBuilder.Entity<User>().Property(x => x.Email).IsRequired();
            modelBuilder.Entity<Role>().Property(x => x.Name).IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
