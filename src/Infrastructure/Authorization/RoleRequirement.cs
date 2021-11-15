using Microsoft.AspNetCore.Authorization;

namespace Axon.Infrastructure.Authorization
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string Role { get; }

        public RoleRequirement(string requiredRole)
        {
            Role = requiredRole;
        }
    }
}
