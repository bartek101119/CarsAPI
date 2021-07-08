using AutoMapper;
using CarsAPI.Entities;
using CarsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPI
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            CreateMap<Car, CarDto>();
            CreateMap<CreateCarDto, Car>();
        }
    }
}
