using AutoMapper;
using Axon.Application.Common.Interfaces;
using Axon.Application.Common.Models;
using Axon.Application.Users.ViewModels;
using Axon.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<ServiceResponse<UserVM>>
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ServiceResponse<UserVM>>
    {
        private readonly IGenericRepository _repository;
        private readonly IMapper _mapper;
        private readonly IApplicationUserService _applicationUserService;


        public UpdateUserCommandHandler(IGenericRepository repository, IMapper mapper, IApplicationUserService applicationUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _applicationUserService = applicationUserService;

        }

        public async Task<ServiceResponse<UserVM>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var entity = await _repository.GetByIdAsync<User>(_applicationUserService.Id);

            entity.Name = request.Name;
            entity.Username = request.Username;
            entity.Password = request.Password;

            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);


            UserVM newUser = _mapper.Map<UserVM>(entity);

            return ServiceResponse.OK(newUser, $"User with Id {entity.Id} Updated Successfully");
           
        }
    }
}
