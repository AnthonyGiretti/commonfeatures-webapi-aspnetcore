using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.AuthorizationHandlers
{
    public class SuperSurveyCreatorRequirement : IAuthorizationRequirement
    {
        public SuperSurveyCreatorRequirement(string role, string group)
        {
            Group = group;
            Role = role;
        }

        public string Role { get; private set; }
        public string Group { get; private set; }
    }
}
