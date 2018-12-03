using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiDemo.AuthorizationHandlers
{
    public class SuperSurveyCreatorAutorizationHandler : AuthorizationHandler<SuperSurveyCreatorRequirement>
    {
        private readonly ILogger<SuperSurveyCreatorAutorizationHandler> _logger;

        public SuperSurveyCreatorAutorizationHandler(ILogger<SuperSurveyCreatorAutorizationHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SuperSurveyCreatorRequirement requirement)
        {
            bool hasRole = false;
            bool hasGroup = false;

            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == requirement.Role))
                _logger.LogInformation("SurveyCreator: required Role not supplied");
            else
                hasRole = true;

            if (!context.User.HasClaim(c => c.Type == "groups" && c.Value == requirement.Group))
                _logger.LogInformation("SurveyCreator: required Group not supplied");
            else
                hasGroup = true;

            if (hasGroup && hasRole)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
