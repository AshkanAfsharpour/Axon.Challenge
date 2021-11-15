using Axon.Application.Common.Interfaces;
using Axon.Application.Common.Models;
using Axon.Application.Users.ViewModels;
using Axon.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.Users.Queries.GetDetail
{
    public class GetUserDetailQuery : IRequest<ServiceResponse<UserVM>>
    {
        public Guid Id { get; set; }
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUserDetailQuery, ServiceResponse<UserVM>>
    {
        private readonly IGenericRepository _repository;

        public GetUsersQueryHandler(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<UserVM>> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetProjectedByIdAsync<User,UserVM>(request.Id);

            if (result == null)
                return ServiceResponse.Failure<UserVM>("No User Found with This Id");

            return ServiceResponse.OK(result);
        }

    }
}
