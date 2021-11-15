using Axon.Application.GitHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Axon.Application.Common.Interfaces.Infrastructure.GitHub
{
    public interface IGitHubServices
    {
        public Task<GitHubProfileVM> GetMyProfile(string accessToken);
        public Task<List<GitHubRepositoryVM>> GetMyRepositories(string accessToken);
        public Task<GitHubProfileVM> GetProfileByUsername(string username);
    }
}
