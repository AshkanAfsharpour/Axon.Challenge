using Axon.Application.Common.Interfaces.Infrastructure.GitHub;
using Axon.Application.GitHub.ViewModels;
using Octokit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Axon.Infrastructure.GitHub.OctoKit
{
    public class GitHubServices : IGitHubServices
    {
        private GitHubClient client = new GitHubClient(new ProductHeaderValue("Axon-Challenge-App"));

        public async Task<GitHubProfileVM> GetMyProfile(string accessToken)
        {

            client.Credentials = new Credentials(accessToken);

            User gitProfile = await client.User.Current();

            return gitProfile.MapToViewModel();

        }

        public async Task<List<GitHubRepositoryVM>> GetMyRepositories(string accessToken)
        {
            client.Credentials = new Credentials(accessToken);

            IReadOnlyList<Repository> myRepositories = await client.Repository.GetAllForCurrent();

            List<GitHubRepositoryVM> gitHubRepositoryVMs = new List<GitHubRepositoryVM>();

            foreach (var repository in myRepositories)
            {
                gitHubRepositoryVMs.Add(repository.MapToViewModel());
            }

            return gitHubRepositoryVMs;
        }

        public async Task<GitHubProfileVM> GetProfileByUsername(string login)
        {
            User gitProfile = await client.User.Get(login);

            return gitProfile.MapToViewModel();
        }
    }
}
