using MediatR;
using Axon.Application.Common.Interfaces;
using Axon.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;
using Axon.Application.GitHub.ViewModels;
using Axon.Domain.Entities;
using Axon.Application.GitHub.Commands.CreateGitHubRepository;

namespace Axon.Application.GitHub.Commands.UpdateGitHubRepository
{
    public class UpdateGitHubRepositoryCommand : INotification
    {
        public GitHubRepositoryVM GitHubRepository { get; set; }
    }

    public class UpdateGitHubRepositoryCommandHandler : INotificationHandler<UpdateGitHubRepositoryCommand>
    {
        private readonly IGenericRepository _repository;
        private readonly IMediator _mediator;

        public UpdateGitHubRepositoryCommandHandler(IGenericRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(UpdateGitHubRepositoryCommand request, CancellationToken cancellationToken)
        {
            var gitRepository = await _repository.GetAsync<GitHubRepository>(x => x.GitId == request.GitHubRepository.GitId);
            
            if (gitRepository == null)
            {
                await _mediator.Publish(new CreateGitHubRepositoryCommand { GitHubRepository = request.GitHubRepository });
            } else
            {
                gitRepository.GitId = request.GitHubRepository.GitId;
                gitRepository.NodeId = request.GitHubRepository.NodeId;
                gitRepository.Name = request.GitHubRepository.Name;
                gitRepository.FullName = request.GitHubRepository.FullName;
                gitRepository.Description = request.GitHubRepository.Description;
                gitRepository.Homepage = request.GitHubRepository.Homepage;
                gitRepository.Language = request.GitHubRepository.Language;
                gitRepository.DefaultBranch = request.GitHubRepository.DefaultBranch;
                gitRepository.Private = request.GitHubRepository.Private;
                gitRepository.Size = request.GitHubRepository.Size;
                gitRepository.WatchersCount = request.GitHubRepository.WatchersCount;
                gitRepository.StargazersCount = request.GitHubRepository.StargazersCount;
                gitRepository.ForksCount = request.GitHubRepository.ForksCount;
                gitRepository.Url = request.GitHubRepository.Url;
                gitRepository.HtmlUrl = request.GitHubRepository.HtmlUrl;
                gitRepository.CloneUrl = request.GitHubRepository.CloneUrl;
                gitRepository.GitUrl = request.GitHubRepository.GitUrl;
                gitRepository.SshUrl = request.GitHubRepository.SshUrl;
                gitRepository.PushedAt = request.GitHubRepository.PushedAt;
                gitRepository.CreatedAt = request.GitHubRepository.CreatedAt;
                gitRepository.ModifiedAt = request.GitHubRepository.UpdatedAt;


                _repository.Update(gitRepository);

                await _repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
