using Axon.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Axon.Infrastructure.Authorization
{
    public class CanPurgePolicyHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly IApplicationUserService _applicationUserService;

        public CanPurgePolicyHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,RoleRequirement requirement)
        {

            if (_applicationUserService.RoleTitle.Equals(requirement.Role))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            
            return Task.CompletedTask;
        }
    }
}
