using Axon.Application.Common.Interfaces;
using Axon.Domain.Entities;
using Axon.Domain.ValueObjects;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IGenericRepository _repository;


        public CreateUserCommandValidator(IGenericRepository repository)
        {
            this._repository = repository;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.")
                .MustAsync(beUniqueUsername).WithMessage("This Username already taken.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");

            RuleFor(x => x.RoleTitle).NotEmpty().WithMessage("Role is required").
                Must(beValidRole).WithMessage("Requested role is invalid");
        }


       

        public async Task<bool> beUniqueUsername(string username, CancellationToken cancellationToken)
        {
            return !await _repository.ExistsAsync<User>(x => x.Username == username);
        }

        public bool beValidRole(string role)
        {
            return UserRoleTypes.GetRoleIdByName(role) != 0;
        }


    }
}
