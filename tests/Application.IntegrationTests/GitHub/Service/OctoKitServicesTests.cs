using Axon.Application.Common.Interfaces.Infrastructure.GitHub;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axon.Application.IntegrationTests.GitHub.Service
{
    using static Testing;
    [TestFixture]
    public class OctoKitServicesTests : TestBase
    {
        [Test]
        public async Task ShouldReturnProfileByAccessToken()
        {
            // Arrange
          
            RunAsAnonymous();
            var accessToken = _configuration.GetValue<string>("GitHubAccessToken");

            // Act

            var response = await  GetService<IGitHubServices>().GetMyProfile(accessToken);

            // Asserts

            response.Should().NotBeNull();

        }
        [Test]
        public async Task ShouldReturnRepositoriesByAccessToken()
        {
            // Arrange
           
            RunAsAnonymous();
            var accessToken = _configuration.GetValue<string>("GitHubAccessToken");
            
            // Act
            
            var response = await GetService<IGitHubServices>().GetMyRepositories(accessToken);
            
            // Asserts

            response.Should().NotBeNull();
        }
        [Test]
        public async Task ShouldReturnProfileByUsername()
        {
            // Arrange
            
            RunAsAnonymous();
            
            // Act
            
            var response = await GetService<IGitHubServices>().GetProfileByUsername("AshkanAfsharpour");
            
            // Asserts
            
            response.Should().NotBeNull();

        }
    }
}
