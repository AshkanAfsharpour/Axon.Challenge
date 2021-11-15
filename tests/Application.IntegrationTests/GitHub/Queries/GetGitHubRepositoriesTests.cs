using Axon.Application.GitHub.Queries.GetMyGitHubProfile;
using Axon.Application.GitHub.Queries.GetMyGitHubRepositories;
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

    public class GetGitHubRepositoriesTests : TestBase
    {
        [Test]
        public async Task ShouldGetRepositoriesByAccessTokenAndSaveOrUpdateTheResultFromGithub()
        {
            // Arrange
            
            RunAsAnonymous();
            var accessToken = _configuration.GetValue<string>("GitHubAccessToken");

            var profileResponse = await SendRequestAsync(new GetMyGitHubProfileQuery { AccessToken = accessToken });


            // Act
            
            var response = await SendRequestAsync(new GetMyGitHubRepositoriesQuery { AccessToken  = accessToken });


            // Asserts

            response.Data.Should().HaveCountGreaterThan(0);


            var savedRepositories = await _iRepository.GetListAsync<GitHubRepository>(x => x.GitUser.GitId == profileResponse.Data.GitId);

            savedRepositories.Should().HaveCount(response.Data.Count);
        }
    }
}
