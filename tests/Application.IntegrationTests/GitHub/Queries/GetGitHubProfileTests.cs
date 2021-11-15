using Axon.Application.GitHub.Queries.GetGitHubProfileByUsername;
using Axon.Application.GitHub.Queries.GetMyGitHubProfile;
using Axon.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axon.Application.IntegrationTests.GitHub.Queries
{
    using static Testing;
    [TestFixture]
    public class GetGitHubProfileTests : TestBase
    {
        [Test]
        public async Task ShouldGetProfileByAccessTokenAndSaveOrUpdateTheResultFromGitHub()
        {
            // Arrange
            
            RunAsAnonymous();
            var accessToken = _configuration.GetValue<string>("GitHubAccessToken");

            // Act
            var response = await SendRequestAsync(new GetMyGitHubProfileQuery { AccessToken = accessToken });


            // Asserts

            response.Data.Should().NotBeNull();
            response.Data.GitId.Should().NotBe(0);


            var savedProfile = await _iRepository.GetAsync<GitHubProfile>(x => x.GitId == response.Data.GitId);

            savedProfile.Should().NotBeNull();
        }

        [Test]
        public async Task ShouldGetProfileByUsername()
        {
            // Arrange

            RunAsAnonymous();

            // Act

            var response = await SendRequestAsync(new GetGitHubProfileByUsernameQuery { Username = "AshkanAfsharpour" });


            // Asserts

            response.Data.Should().NotBeNull();
        }
    }
}
