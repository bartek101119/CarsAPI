using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPI.Authorization
{
    public class MinimumTwoCarsRequirement : IAuthorizationRequirement
    {
        public MinimumTwoCarsRequirement(int MinimumTwoCars)
        {
            this.MinimumTwoCars = MinimumTwoCars;
        }
        public int MinimumTwoCars { get; }
    }
}
