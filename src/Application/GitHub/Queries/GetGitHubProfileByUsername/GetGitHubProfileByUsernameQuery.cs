using MediatR;
using Axon.Application.Common.Interfaces;
using Axon.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;
using Axon.Application.Common.Interfaces.Infrastructure.GitHub;
using Axon.Application.GitHub.ViewModels;
using Axon.Application.Common.Exceptions;

namespace Axon.Application.GitHub.Queries.GetGitHubProfileByUsername
{
    public class GetGitHubProfileByUsernameQuery : IRequest<ServiceResponse<GitHubProfileVM>>
    {
        public string Username { get; set; }
    }

    public class GetGitHubProfileByUsernameQueryHandler : IRequestHandler<GetGitHubProfileByUsernameQuery, ServiceResponse<GitHubProfileVM>>
    {
        private readonly IGenericRepository _repository;
        private readonly IGitHubServices _gitHubServices;

        public GetGitHubProfileByUsernameQueryHandler(IGenericRepository repository, IGitHubServices gitHubServices)
        {
            _repository = repository;
            _gitHubServices = gitHubServices;
        }

        public IGitHubServices GitHubServices { get; }

        public async Task<ServiceResponse<GitHubProfileVM>> Handle(GetGitHubProfileByUsernameQuery request, CancellationToken cancellationToken)
        {
            var profile = await _gitHubServices.GetProfileByUsername(request.Username);

            if (profile == null) throw new NotFoundException(request.Username, "Username");

            return ServiceResponse.OK(profile);
        }
    }
}
