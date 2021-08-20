using CarsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPI.Entities
{
    public class CarCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LegalForm { get; set; }
        public string REGON { get; set; }
        public string NIP { get; set; }
        public DateTime DateOfCommencementOfActivity { get; set; }
        public virtual List<Car> Cars { get; set; }

    }
}
