using MediatR;
using Axon.Application.Common.Interfaces;
using Axon.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;
using Axon.Application.GitHub.ViewModels;
using Axon.Domain.Entities;
using Axon.Application.GitHub.Commands.CreateGitHubProfile;

namespace Axon.Application.GitHub.Commands.UpdateGitHubProfile
{
    public class UpdateGitHubProfileCommand : INotification
    {
        public GitHubProfileVM GitHubProfile { get; set; }

    }

    public class UpdateGitHubProfileCommandHandler : INotificationHandler<UpdateGitHubProfileCommand>
    {
        private readonly IGenericRepository _repository;
        private readonly IMediator _mediator;

        public UpdateGitHubProfileCommandHandler(IGenericRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(UpdateGitHubProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await _repository.GetAsync<GitHubProfile>(x => x.GitId == request.GitHubProfile.GitId);

            if (profile == null)
            {
                await _mediator.Publish(new CreateGitHubProfileCommand { GitHubProfile = request.GitHubProfile });
            } else
            {
                profile.GitId = request.GitHubProfile.GitId;
                profile.NodeId = request.GitHubProfile.NodeId;
                profile.Name = request.GitHubProfile.Name;
                profile.Login = request.GitHubProfile.Login;
                profile.Email = request.GitHubProfile.Email;
                profile.Blog = request.GitHubProfile.Blog;
                profile.Bio = request.GitHubProfile.Bio;
                profile.Location = request.GitHubProfile.Location;
                profile.AvatarUrl = request.GitHubProfile.AvatarUrl;
                profile.Url = request.GitHubProfile.Url;
                profile.HtmlUrl = request.GitHubProfile.HtmlUrl;
                profile.Followers = request.GitHubProfile.Followers;
                profile.Following = request.GitHubProfile.Following;
                profile.Collaborators = request.GitHubProfile.Collaborators;
                profile.DiskUsage = request.GitHubProfile.DiskUsage;
                profile.Company = request.GitHubProfile.Company;
                profile.Suspended = request.GitHubProfile.Suspended;
                profile.SuspendedAt = request.GitHubProfile.SuspendedAt;
                profile.ModifiedAt = request.GitHubProfile.UpdatedAt;

                _repository.Update(profile);

                await _repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
