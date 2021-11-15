using Axon.Application.Common.Interfaces;
using Axon.Infrastructure.Authorization;
using Axon.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Axon.Application.Common.Interfaces.Infrastructure.GitHub;
using Axon.Infrastructure.GitHub.OctoKit;

namespace Axon.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddJwtAuthentication(configuration);
            services.AddJwtAuthorization();


            services.AddScoped<IjwtServices, JwtServices>();

            services.AddScoped<IAuthorizationHandler, CanPurgePolicyHandler>();

            services.AddScoped<IGitHubServices, GitHubServices>();

            return services;
        }

    }
}
