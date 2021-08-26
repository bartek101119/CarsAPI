using CarsAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarsAPI.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, CarCompany>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, CarCompany carCompany)
        {
            if(requirement.ResourceOperation == ResourceOperation.Create || 
                requirement.ResourceOperation == ResourceOperation.Read)
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if(carCompany.CreateById == int.Parse(userId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
