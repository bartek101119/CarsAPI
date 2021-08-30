using AutoMapper;
using CarsAPI.Authorization;
using CarsAPI.Entities;
using CarsAPI.Exceptions;
using CarsAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IAuthorizationService authorizationService;
        private readonly IUserContextService userContextService;

        public CarCompanyService(CarDbContext context, IMapper mapper, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            this.context = context;
            this.mapper = mapper;
            this.authorizationService = authorizationService;
            this.userContextService = userContextService;
        }
        public CarCompanyDto GetCompany(int id)
        {
            var company = context.CarCompany.Include(d => d.Cars).FirstOrDefault(c => c.Id == id);

            if (company is null)
                return null;

            var companyDto = mapper.Map<CarCompanyDto>(company);

            return companyDto;

        }

        public IEnumerable<CarCompanyDto> GetCompanies()
        {
            var companies = context.CarCompany.Include(d => d.Cars).ToList();

            var companiesDtos = mapper.Map<List<CarCompanyDto>>(companies);

            return companiesDtos;
        }

        public int NewCompany(CreateCarCompanyDto dto)
        {
            var company = mapper.Map<CarCompany>(dto);
            company.CreateById = userContextService.GetUserId();

            context.CarCompany.Add(company);
            context.SaveChanges();

            return company.Id;

        }

        public bool DeletedCompany(int id)
        {
            var deleted = context.CarCompany.FirstOrDefault(c => c.Id == id);

            if (deleted is null)
                return false;

            var authorizationResult = authorizationService.AuthorizeAsync(userContextService.User, deleted, new ResourceOperationRequirement(ResourceOperation.Delete
                )).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            context.CarCompany.RemoveRange(deleted);
            context.SaveChanges();

            return true;
        }

        public bool Update(CreateCarCompanyDto dto, int id)
        {
            var updated = context.CarCompany.FirstOrDefault(c => c.Id == id);

            if (updated is null)
                return false;

            var authorizationResult = authorizationService.AuthorizeAsync(userContextService.User, updated, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            updated.LegalForm = dto.LegalForm;
            updated.Name = dto.Name;
            updated.REGON = dto.REGON;
            updated.DateOfCommencementOfActivity = dto.DateOfCommencementOfActivity;
            updated.NIP = dto.NIP;

            context.SaveChanges();

            return true;
        }

    }
}
