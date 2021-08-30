using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarsAPI.Services
{
    public interface IUserContextService
    {
        int? GetUserId();
        ClaimsPrincipal User { get; }
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => httpContextAccessor.HttpContext?.User;

        public int? GetUserId()
        {
            if (User is null)
                return null; 

            return (int?)int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
