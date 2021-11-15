using Axon.Application.Common.Interfaces;
using Axon.Application.Common.Interfaces.Infrastructure.GitHub;
using Axon.Application.Common.Models;
using Axon.Application.GitHub.Commands.CreateGitHubRepository;
using Axon.Application.GitHub.Commands.UpdateGitHubRepository;
using Axon.Application.GitHub.ViewModels;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.GitHub.Queries.GetMyGitHubRepositories
{
    public class GetMyGitHubRepositoriesQuery : IRequest<ServiceResponse<List<GitHubRepositoryVM>>>
    {
        public string AccessToken { get; set; }
    }

    public class GetMyGitHubRepositoriesQueryHandler : IRequestHandler<GetMyGitHubRepositoriesQuery, ServiceResponse<List<GitHubRepositoryVM>>>
    {
        private readonly IGenericRepository _repository;
        private readonly IGitHubServices _gitHubServices;
        private readonly IMediator _mediator;

        public GetMyGitHubRepositoriesQueryHandler(IGenericRepository repository, IGitHubServices gitHubServices, IMediator mediator)
        {
            _repository = repository;
            _gitHubServices = gitHubServices;
            _mediator = mediator;
        }

        public async Task<ServiceResponse<List<GitHubRepositoryVM>>> Handle(GetMyGitHubRepositoriesQuery request, CancellationToken cancellationToken)
        {
            var repositories = await _gitHubServices.GetMyRepositories(request.AccessToken);

            foreach (var repository in repositories)
            {
                await _mediator.Publish(new UpdateGitHubRepositoryCommand { GitHubRepository = repository});
            }

            return ServiceResponse.OK(repositories);
        }
    }
}
