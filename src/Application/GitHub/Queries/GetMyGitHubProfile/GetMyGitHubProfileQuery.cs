using Axon.Application.Common.Interfaces;
using Axon.Application.Common.Interfaces.Infrastructure.GitHub;
using Axon.Application.Common.Models;
using Axon.Application.GitHub.Commands.UpdateGitHubProfile;
using Axon.Application.GitHub.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.GitHub.Queries.GetMyGitHubProfile
{
    public class GetMyGitHubProfileQuery : IRequest<ServiceResponse<GitHubProfileVM>>
    {
        public string AccessToken { get; set; }
    }

    public class GetMyGitHubProfileQueryHandler : IRequestHandler<GetMyGitHubProfileQuery, ServiceResponse<GitHubProfileVM>>
    {
        private readonly IGenericRepository _repository;
        private readonly IGitHubServices _gitHubServices;
        private readonly IMediator _mediator;

        public GetMyGitHubProfileQueryHandler(IGenericRepository repository, IGitHubServices gitHubServices, IMediator mediator)
        {
            _repository = repository;
            _gitHubServices = gitHubServices;
            _mediator = mediator;
        }

        public async Task<ServiceResponse<GitHubProfileVM>> Handle(GetMyGitHubProfileQuery request, CancellationToken cancellationToken)
        {
       
            var myGitHubProfile = await _gitHubServices.GetMyProfile(request.AccessToken);

            await _mediator.Publish(new UpdateGitHubProfileCommand { GitHubProfile = myGitHubProfile});

            return ServiceResponse.OK(myGitHubProfile);
        }
    }
}
