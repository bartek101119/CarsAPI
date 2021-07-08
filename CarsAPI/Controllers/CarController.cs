using CarsAPI.Models;
using CarsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPI.Controllers
{
    [Route("/api/cars")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService service;

        public CarController(ICarService service)
        {
            this.service = service;
        }
        [HttpPost]
        public ActionResult Post([FromBody] CreateCarDto dto)
        {
            var carId = service.Create(dto);

            return Created($"/api/cars/{carId}", null);
        }

        public ActionResult<IEnumerable<CarDto>> GetAll()
        {
            var allCarsDtos = service.GetAll();

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
