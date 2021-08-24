using CarsAPI.Models;
using CarsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPI.Controllers
{
    [Route("/api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService service;

        public AccountController(IAccountService service)
        {
            this.service = service;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody]RegisterUserDto dto)
        {
            service.NewUser(dto);
            return Ok();

        }
    }
}
