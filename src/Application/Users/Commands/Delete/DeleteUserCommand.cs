using AutoMapper;
using Axon.Application.Common.Interfaces;
using Axon.Application.Common.Models;
using Axon.Application.Users.ViewModels;
using Axon.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<ServiceResponse<UserVM>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ServiceResponse<UserVM>>
    {
        private readonly IGenericRepository _repository;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(IGenericRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<UserVM>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync<User>(request.Id);

            _repository.Delete(entity);

            await _repository.SaveChangesAsync(cancellationToken);

            return ServiceResponse.OK(_mapper.Map<UserVM>(entity), $"User with Id {entity.Id} Deleted Successfully");
        }
    }
}
