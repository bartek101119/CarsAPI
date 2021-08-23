using CarsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPI
{
    public class CarCompanySeeder
    {
        private readonly CarDbContext context;

        public CarCompanySeeder(CarDbContext context)
        {
            this.context = context;
        }
        public void Seed()
        {
            if (context.Database.CanConnect())
            {
                if (!context.Roles.Any())
                {
                    var roles = GetRoles();
                    context.Roles.AddRange(roles);
                    context.SaveChanges();
                }
            }

        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Admin"
                },
                new Role()
                {
                    Name = "Mod"
                }
            };
            return roles;
        }
    }
}
