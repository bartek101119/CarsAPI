using AutoMapper;
using CarsAPI.Entities;
using CarsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPI.Services
{
    public interface ICarService
    {
        public int? Create(CreateCarDto dto, int carCompanyId);
        bool DeleteCar(int id);
        IEnumerable<CarDto> GetAll(int carCompanyId);
        CarDto GetOne(int id);
        bool Update(UpdateCarDto dto, int id);
    }
    public class CarService : ICarService
    {
        private readonly CarDbContext dbContext;
        private readonly IMapper mapper;

        public CarService(CarDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public int? Create(CreateCarDto dto, int carCompanyId)
        {
            var carCompany = dbContext.CarCompany.FirstOrDefault(c => c.Id == carCompanyId);

            if (carCompany is null)
                return null;

            var car = mapper.Map<Car>(dto);

            car.CarCompanyId = carCompanyId;

            dbContext.Cars.Add(car);
            dbContext.SaveChanges();

            return car.Id;
        }

        public IEnumerable<CarDto> GetAll(int carCompanyId)
        {
            var carCompany = dbContext.CarCompany.Include(c => c.Cars).FirstOrDefault(c => c.Id == carCompanyId);

            if (carCompany is null)
                return null;


            var allCarsDtos = mapper.Map<List<CarDto>>(carCompany.Cars);

            return allCarsDtos;
        }

        public CarDto GetOne(int id)
        {
            var car = dbContext.Cars.FirstOrDefault(c => c.Id == id);

            if (car is null)
                return null;

            var carDto = mapper.Map<CarDto>(car);

            return carDto;
        }

        public bool DeleteCar(int id)
        {
            var car = dbContext.Cars.FirstOrDefault(c => c.Id == id);

            if (car is null)
                return false;

            dbContext.Cars.RemoveRange(car);
            dbContext.SaveChanges();

            return true;
        }

        public bool Update(UpdateCarDto dto, int id)
        {
            var car = dbContext.Cars.FirstOrDefault(c => c.Id == id);

            if (car is null)
                return false;

            car.ClientFirstName = dto.ClientFirstName;
            car.Color = dto.Color;
            car.Cost = dto.Cost;
            car.IsFullyDamaged = dto.IsFullyDamaged;
            car.Model = dto.Model;
            car.Surname = dto.Surname;
            car.Year = dto.Year;

            dbContext.SaveChanges();

            return true;

        }
        
    }
}
