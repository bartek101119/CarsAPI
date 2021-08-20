using CarsAPI.Entities;
using CarsAPI.Models;
using CarsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPI.Controllers
{
    [Route("/api/carCompany")]
    [ApiController]
    public class CarCompanyController : ControllerBase
    {
        private readonly ICarCompanyService service;

        public CarCompanyController(ICarCompanyService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<CarCompanyDto> Get([FromRoute]int id)
        {
            var company = service.GetCompany(id);

            if (company is null)
                return NotFound();

            return Ok(company);
            
        }

        [HttpGet]
        public ActionResult<IEnumerable<CarCompany>> GetAll()
        {
            var companies = service.GetCompanies();

            return Ok(companies);
        }

        [HttpPost]
        public ActionResult Post([FromBody] CreateCarCompanyDto dto)
        {
            var companyId = service.NewCompany(dto);

            return Created($"/api/carCompany/{companyId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            var deleted = service.DeletedCompany(id);

            if (deleted)
                return NoContent();

            return NotFound();
        }

    }
}
