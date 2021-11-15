using Axon.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Axon.Presistence;
using Respawn;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Axon.WebApi;
using Axon.WebApi.Services;
using Axon.Application.Users.Commands.CreateUser;
using Axon.Domain.ValueObjects;
using System.IdentityModel.Tokens.Jwt;
using Axon.Application.IntegrationTests.Models;
using Axon.Domain.Entities;

namespace Axon.Application.IntegrationTests
{
    [SetUpFixture]
    public class Testing
    {
        public static IConfiguration _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;
        private static MockApplicationUserService _currentUser = new MockApplicationUserService();
        internal static IGenericRepository _iRepository;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var services = new ServiceCollection();

            var startup = new Startup(_configuration);

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.ApplicationName == "Axon.WebApi" &&
                w.EnvironmentName == "Development"
            ));

            services.AddLogging();

            startup.ConfigureServices(services);

            var applicationUserServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(IApplicationUserService));

            services.Remove(applicationUserServiceDescriptor);

            services.AddTransient(provider =>
              Mock.Of<IApplicationUserService>(x =>
                    x.Id == _currentUser.Id &&
                    x.RoleTitle == _currentUser.Role &&
                    x.SessionId == _currentUser.SessionId &&
                    x.IsAuthenticated == _currentUser.IsAuthenticated));


            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" }
            };



            EnsureDatabase();

        }

        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<AxonContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
        public static async Task ResetState()
        {
            await _checkpoint.Reset(_configuration.GetConnectionString("CSLocal"));
        }
        public static void RunAsAnonymous()
        {
            _currentUser = new MockApplicationUserService();
            SetRepository();
        }

        public async static Task<Guid> RunAsUserAsync(string role)
        {
            var createdUserResponse = await SendRequestAsync(new CreateUserCommand
            {
                Username = $"test {role}",
                Name = $"test {role}",
                Password = "SuperSecret",
                RoleTitle = role
            });

            _currentUser = new MockApplicationUserService
            {
                Id = createdUserResponse.Data.Id,
                SessionId = createdUserResponse.Data.SessionId,
                Role = UserRoleTypes.GetRoleNameById(createdUserResponse.Data.RoleId),
                IsAuthenticated = true,
            };

            SetRepository();
            return createdUserResponse.Data.Id;
        }
        
        public static T GetService<T>()
        {
            using var scope = _scopeFactory.CreateScope();
            return scope.ServiceProvider.GetService<T>();
        }
        public static void SetRepository()
        {
            _iRepository = _scopeFactory.CreateScope().ServiceProvider.GetService<IGenericRepository>();
        }


        public static async Task<TResponse> SendRequestAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }
    }
}