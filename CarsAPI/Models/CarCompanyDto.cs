using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPI.Models
{
    public class CarCompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LegalForm { get; set; }
        public string REGON { get; set; }
        public string NIP { get; set; }
        public DateTime DateOfCommencementOfActivity { get; set; }
    }
}
