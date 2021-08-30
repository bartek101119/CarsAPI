using CarsAPI.Models;
using CarsAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPI.Controllers
{
    [Route("/api/carCompany/{carCompanyId}/cars")]
    [ApiController]
    [Authorize]
    public class CarController : ControllerBase
    {
        private readonly ICarService service;

        public CarController(ICarService service)
        {
            this.service = service;
        }
        [HttpPost]
        public ActionResult Post([FromBody] CreateCarDto dto, [FromRoute]int carCompanyId)
        {
            var carId = service.Create(dto, carCompanyId);

            return Created($"/api/{carCompanyId}/cars/{carId}", null);
        }

        [HttpGet]
        [Authorize(Policy = "MinimumTwoCars")]
        public ActionResult<IEnumerable<CarDto>> GetAll([FromRoute]int carCompanyId)
        {
            var allCarsDtos = service.GetAll(carCompanyId);

            return Ok(allCarsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<CarDto> GetOne([FromRoute]int id)
        {
            var carDto = service.GetOne(id);

            if(carDto is null)
            {
                return NotFound();
            }

            return Ok(carDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Mod")]
        public ActionResult Delete([FromRoute]int id)
        {
            var carDeleted = service.DeleteCar(id);

            if (!carDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute]int id, [FromBody]UpdateCarDto dto)
        {
            var carUpdated = service.Update(dto, id);

            if (!carUpdated)
            {
                return NotFound();
            }

            return Ok();
                
        }

    }
}
