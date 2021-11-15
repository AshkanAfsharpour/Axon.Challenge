using MediatR;
using Axon.Application.Common.Interfaces;
using Axon.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;
using Axon.Domain.Entities;
using System;
using Axon.Application.GitHub.ViewModels;

namespace Axon.Application.GitHub.Commands.CreateGitHubRepository
{
    public class CreateGitHubRepositoryCommand : INotification
    {
        public GitHubRepositoryVM GitHubRepository { get; set; }
    }

    public class CreateGitHubRepositoryCommandHandler : INotificationHandler<CreateGitHubRepositoryCommand>
    {
        private readonly IGenericRepository _repository;

        public CreateGitHubRepositoryCommandHandler(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateGitHubRepositoryCommand request, CancellationToken cancellationToken)
        {
            var gitUser = await _repository.GetAsync<GitHubProfile>(x => x.GitId == request.GitHubRepository.GitUserId);
            GitHubRepository githubRepository = new GitHubRepository
            {
                GitId = request.GitHubRepository.GitId,
                NodeId = request.GitHubRepository.NodeId,
                Name = request.GitHubRepository.Name,
                FullName = request.GitHubRepository.FullName,
                Description = request.GitHubRepository.Description,
                Homepage = request.GitHubRepository.Homepage,
                Language = request.GitHubRepository.Language,
                DefaultBranch = request.GitHubRepository.DefaultBranch,
                Private = request.GitHubRepository.Private,
                Size = request.GitHubRepository.Size,
                WatchersCount = request.GitHubRepository.WatchersCount,
                StargazersCount = request.GitHubRepository.StargazersCount,
                ForksCount = request.GitHubRepository.ForksCount,
                Url = request.GitHubRepository.Url,
                HtmlUrl = request.GitHubRepository.HtmlUrl,
                CloneUrl = request.GitHubRepository.CloneUrl,
                GitUrl = request.GitHubRepository.GitUrl,
                SshUrl = request.GitHubRepository.SshUrl,
                PushedAt = request.GitHubRepository.PushedAt,
                CreatedAt = request.GitHubRepository.CreatedAt,
                ModifiedAt = request.GitHubRepository.UpdatedAt,
                GitUser = gitUser,
            };

            await _repository.InsertAsync(githubRepository);

            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
