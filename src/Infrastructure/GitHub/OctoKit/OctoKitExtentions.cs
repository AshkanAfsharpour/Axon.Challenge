using Axon.Application.GitHub.ViewModels;
using Octokit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Axon.Infrastructure.GitHub.OctoKit
{
    public static class OctoKitExtentions
    {
        public static GitHubProfileVM MapToViewModel(this User gitHubUser)
        {
            return new GitHubProfileVM
            {
                GitId = gitHubUser.Id,
                NodeId = gitHubUser.NodeId,
                Name = gitHubUser.Name,
                Login = gitHubUser.Login,
                Email = gitHubUser.Email,
                Blog = gitHubUser.Blog,
                Bio = gitHubUser.Bio,
                Location = gitHubUser.Location,
                AvatarUrl = gitHubUser.AvatarUrl,
                Url = gitHubUser.Url,
                HtmlUrl = gitHubUser.HtmlUrl,
                Followers = gitHubUser.Followers,
                Following = gitHubUser.Following,
                Collaborators = gitHubUser.Collaborators,
                DiskUsage = gitHubUser.DiskUsage,
                Company = gitHubUser.Company,
                Suspended = gitHubUser.Suspended,
                SuspendedAt = gitHubUser.SuspendedAt?.DateTime,
                UpdatedAt = gitHubUser.UpdatedAt.DateTime,
                CreatedAt = gitHubUser.CreatedAt.DateTime,
            };
        }

        public static GitHubRepositoryVM MapToViewModel(this Repository repository)
        {
            return new GitHubRepositoryVM
            {
                GitId = repository.Id,
                GitUserId = repository.Owner.Id,
                NodeId = repository.NodeId,
                Name = repository.Name,
                FullName = repository.FullName,
                Description = repository.Description,
                Homepage = repository.Homepage,
                Language = repository.Language,
                DefaultBranch = repository.DefaultBranch,
                Private = repository.Private,
                Size = repository.Size,
                WatchersCount = repository.WatchersCount,
                StargazersCount = repository.StargazersCount,
                ForksCount = repository.ForksCount,
                Url = repository.Url,
                HtmlUrl = repository.HtmlUrl,
                CloneUrl = repository.CloneUrl,
                GitUrl = repository.GitUrl,
                SshUrl = repository.SshUrl,
                PushedAt = repository.PushedAt?.DateTime,
                CreatedAt = repository.CreatedAt.DateTime,
                UpdatedAt = repository.UpdatedAt.DateTime,
            };
        }

    }
}
