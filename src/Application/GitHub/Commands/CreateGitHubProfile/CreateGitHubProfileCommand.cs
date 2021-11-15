using MediatR;
using Axon.Application.Common.Interfaces;
using Axon.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;
using System;
using Axon.Domain.Entities;
using Axon.Application.GitHub.ViewModels;

namespace Axon.Application.GitHub.Commands.CreateGitHubProfile
{
    public class CreateGitHubProfileCommand : INotification
    {
        public GitHubProfileVM GitHubProfile { get; set; }

    }

    public class CreateGitHubProfileCommandHandler : INotificationHandler<CreateGitHubProfileCommand>
    {
        private readonly IGenericRepository _repository;

        public CreateGitHubProfileCommandHandler(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateGitHubProfileCommand request, CancellationToken cancellationToken)
        {
            GitHubProfile profile = new GitHubProfile
            {
                GitId = request.GitHubProfile.GitId,
                NodeId = request.GitHubProfile.NodeId,
                Name = request.GitHubProfile.Name,
                Login = request.GitHubProfile.Login,
                Email = request.GitHubProfile.Email,
                Blog = request.GitHubProfile.Blog,
                Bio = request.GitHubProfile.Bio,
                Location = request.GitHubProfile.Location,
                AvatarUrl = request.GitHubProfile.AvatarUrl,
                Url = request.GitHubProfile.Url,
                HtmlUrl = request.GitHubProfile.HtmlUrl,
                Followers = request.GitHubProfile.Followers,
                Following = request.GitHubProfile.Following,
                Collaborators = request.GitHubProfile.Collaborators,
                DiskUsage = request.GitHubProfile.DiskUsage,
                Company = request.GitHubProfile.Company,
                Suspended = request.GitHubProfile.Suspended,
                SuspendedAt = request.GitHubProfile.SuspendedAt,
                ModifiedAt = request.GitHubProfile.UpdatedAt,
                CreatedAt = request.GitHubProfile.CreatedAt,
            };

            await _repository.InsertAsync(profile);

            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
