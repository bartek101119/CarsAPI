using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPI.Models
{
    public class UpdateCarDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string ClientFirstName { get; set; }
        public string Surname { get; set; }
        public string Cost { get; set; }
        public bool IsFullyDamaged { get; set; }
        public int Year { get; set; }
    }
}
