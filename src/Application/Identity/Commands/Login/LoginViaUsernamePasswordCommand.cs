using AutoMapper;
using Axon.Application.Common.Exceptions;
using Axon.Application.Common.Interfaces;
using Axon.Application.Common.Models;
using Axon.Application.Identity.ViewModels;
using Axon.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.Identity.Commands.Login
{
    public class LoginViaUsernamePasswordCommand : IRequest<ServiceResponse<LoginVM>>
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }

    public class CreateUserCommandHandler : IRequestHandler<LoginViaUsernamePasswordCommand, ServiceResponse<LoginVM>>
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

        public async Task<ServiceResponse<LoginVM>> Handle(LoginViaUsernamePasswordCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAsync<User>(x => x.Username == request.Username && x.Password == request.Password);

            if (entity == null)
            {
                throw new AuthorizationException("Username and Password Does not Match");
            }


            entity.RefreshToken = _jwtServices.GenerateRefreshToken();
            entity.SessionId = Guid.NewGuid();

            _repository.Update(entity);

            await _repository.SaveChangesAsync(cancellationToken);



            LoginVM newUser = _mapper.Map<LoginVM>(entity);

            newUser.Token = _jwtServices.IssueToken(entity.Id, entity.RoleId, entity.SessionId);

            return ServiceResponse.OK(newUser);

        }
    }
}
