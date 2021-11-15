using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Axon.Infrastructure.Authorization
{
    public static class JwtAuthorizationOptions
    {
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
                auth.AddPolicy("CanPurge", policy =>
                    policy.Requirements.Add(new RoleRequirement("Admin"))
                );
            });
            return services;
        }
    }
}
