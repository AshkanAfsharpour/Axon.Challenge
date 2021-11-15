using AutoMapper;
using Axon.Application.Common.Models;
using Axon.Application.Common.Interfaces;
using Axon.Application.Users.ViewModels;
using Axon.Domain.Entities;
using Axon.Domain.ValueObjects;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<ServiceResponse<ProfileVM>>
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RoleTitle { get; set; }

    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ServiceResponse<ProfileVM>>
    {
        private readonly IGenericRepository _repository;
        private readonly IMapper _mapper;
        private readonly IjwtServices _jwtServices;

        public CreateUserCommandHandler(IGenericRepository repository, IMapper mapper, IjwtServices jwtServices)
        {
            _repository = repository;
            _mapper = mapper;
            _jwtServices = jwtServices;
        }

        public async Task<ServiceResponse<ProfileVM>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var entity = new User
            {
                Name = request.Name,
                Username = request.Username,
                Password = request.Password,
                RefreshToken = _jwtServices.GenerateRefreshToken(),
                RoleId = UserRoleTypes.GetRoleIdByName(request.RoleTitle),
                SessionId = Guid.NewGuid()
            };

            await _repository.InsertAsync(entity);

            await _repository.SaveChangesAsync(cancellationToken);


            ProfileVM newUser = _mapper.Map<ProfileVM>(entity);

            newUser.Token = _jwtServices.IssueToken(entity.Id, entity.RoleId,entity.SessionId);

            return ServiceResponse.OK(newUser, $"User with Id {entity.Id} Added Successfully");
           
        }
    }
}
