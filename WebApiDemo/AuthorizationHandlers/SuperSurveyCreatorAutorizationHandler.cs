using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiDemo.AuthorizationHandlers
{
    public class SuperSurveyCreatorAutorizationHandler : AuthorizationHandler<object>
    {
        private readonly ILogger<SuperSurveyCreatorAutorizationHandler> _logger;

        public SuperSurveyCreatorAutorizationHandler(ILogger<SuperSurveyCreatorAutorizationHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, object requirement)
        {
            bool hasRole = false;
            bool hasGroup = false;

            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "SurveyCreator"))
                _logger.LogInformation("SurveyCreator: required Role not supplied");
            else
                hasRole = true;

            if (!context.User.HasClaim(c => c.Type == "groups" && c.Value == "8115e3be-ac7a-4886-a1e6-5b6aaf810a8f"))
                _logger.LogInformation("SurveyCreator: required Group not supplied");
            else
                hasGroup = true;

            if (hasGroup && hasRole)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
