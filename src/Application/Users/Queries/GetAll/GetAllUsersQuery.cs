using AutoMapper;
using Axon.Application.Common.Interfaces;
using Axon.Application.Common.Interfaces.Infrastructure.GitHub;
using Axon.Application.Common.Models;
using Axon.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.Users.Queries.GetUsers
{
    public class GetAllUsersQuery : PaginatedRequest,IRequest<ServiceResponse<PaginatedList<User>>>
    {
       
    }

    public class GetUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ServiceResponse<PaginatedList<User>>>
    {
        private readonly IGenericRepository _repository;
        private readonly IMapper _mapper;
        private readonly IGitHubServices _gitHubServices;

        public GetUsersQueryHandler(IGenericRepository repository, IMapper mapper, IGitHubServices gitHubServices)
        {
            _repository = repository;
            _mapper = mapper;
            _gitHubServices = gitHubServices;
        }

        public async Task<ServiceResponse<PaginatedList<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var specification = new Specification<User>( x => x.OrderBy(e => e.Id), request.PageIndex, request.PageSize);
            
            var result = await _repository.GetPaginatedListAsync(specification);

            if (result.Items.Count == 0)
                return ServiceResponse.Failure<PaginatedList<User>>("No User Found");

            return ServiceResponse.OK(result);
        }

    }
}
