using AutoMapper;
using CarsAPI.Entities;
using CarsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPI.Services
{
    public interface ICarCompanyService
    {
        IEnumerable<CarCompanyDto> GetCompanies();
        CarCompanyDto GetCompany(int id);
    }
    public class CarCompanyService : ICarCompanyService
    {
        private readonly CarDbContext context;
        private readonly IMapper mapper;

        public CarCompanyService(CarDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public CarCompanyDto GetCompany(int id)
        {
            var company = context.CarCompanies.FirstOrDefault(c => c.Id == id);

            if (company is null)
                return null;

            var companyDto = mapper.Map<CarCompanyDto>(company);

            return companyDto;

        }

        public IEnumerable<CarCompanyDto> GetCompanies()
        {
            var companies = context.CarCompanies.ToList();

            var companiesDtos = mapper.Map<List<CarCompanyDto>>(companies);

            return companiesDtos;
        }


    }
}
