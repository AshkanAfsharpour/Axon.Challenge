using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Axon.Application.Common.Interfaces;

namespace Axon.Presistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
           

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<AxonContext>(options =>
                    options.UseInMemoryDatabase("AxonDb"));
            }
            else
            {
                services.AddDbContext<AxonContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CSLocal"),
                        b => b.MigrationsAssembly(typeof(AxonContext).Assembly.FullName)));


            }
            services.AddScoped<IAxonContext>(provider => provider.GetService<AxonContext>());

            services.AddScoped<IGenericRepository, GenericRepository>(provider => new GenericRepository(provider.GetService<AxonContext>(),provider.GetService<AutoMapper.IConfigurationProvider>()));


            return services;
        }
    }
}
