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
    public interface ICarCompanyService
    {
        bool DeletedCompany(int id);
        IEnumerable<CarCompanyDto> GetCompanies();
        CarCompanyDto GetCompany(int id);
        int NewCompany(CreateCarCompanyDto dto);
        bool Update(CreateCarCompanyDto dto, int id);
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
            var company = context.CarCompanies.Include(d => d.Cars).FirstOrDefault(c => c.Id == id);

            if (company is null)
                return null;

            var companyDto = mapper.Map<CarCompanyDto>(company);

            return companyDto;

        }

        public IEnumerable<CarCompanyDto> GetCompanies()
        {
            var companies = context.CarCompanies.Include(d => d.Cars).ToList();

            var companiesDtos = mapper.Map<List<CarCompanyDto>>(companies);

            return companiesDtos;
        }

        public int NewCompany(CreateCarCompanyDto dto)
        {
            var company = mapper.Map<CarCompany>(dto);

            context.CarCompanies.Add(company);
            context.SaveChanges();

            return company.Id;

        }

        public bool DeletedCompany(int id)
        {
            var deleted = context.CarCompanies.FirstOrDefault(c => c.Id == id);

            if (deleted is null)
                return false;

            context.CarCompanies.RemoveRange(deleted);
            context.SaveChanges();

            return true;
        }

        public bool Update(CreateCarCompanyDto dto, int id)
        {
            var updated = context.CarCompanies.FirstOrDefault(c => c.Id == id);

            if (updated is null)
                return false;

            updated.LegalForm = dto.LegalForm;
            updated.Name = dto.Name;
            updated.REGON = dto.REGON;
            updated.DateOfCommencementOfActivity = dto.DateOfCommencementOfActivity;

            context.SaveChanges();

            return true;
        }

    }
}
