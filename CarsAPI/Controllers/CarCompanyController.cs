using CarsAPI.Entities;
using CarsAPI.Models;
using CarsAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarsAPI.Controllers
{
    [Route("/api/carCompany")]
    [ApiController]
    [Authorize]
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
            var userId = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var companyId = service.NewCompany(dto, userId);

            return Created($"/api/carCompany/{companyId}", null);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Mod")]
        public ActionResult Delete([FromRoute]int id)
        {
            var deleted = service.DeletedCompany(id, User);

            if (deleted)
                return NoContent();

            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] CreateCarCompanyDto dto, [FromRoute]int id)
        {
            var updated = service.Update(dto, id, User);

            if (!updated)
                return NotFound();

            return NoContent();
        }

    }
}
