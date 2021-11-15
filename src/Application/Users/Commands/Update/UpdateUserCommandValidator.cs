using Axon.Application.Common.Interfaces;
using Axon.Domain.Entities;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IGenericRepository _repository;
        private readonly IApplicationUserService _applicationUserService;

        public UpdateUserCommandValidator(IGenericRepository repository, IApplicationUserService applicationUserService)
        {
            this._repository = repository;
            this._applicationUserService = applicationUserService;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.")
                .MustAsync(beUniqueUsername).WithMessage("This Username already taken.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }

        public async Task<bool> beUniqueUsername(string username, CancellationToken cancellationToken)
        {
            return !await _repository.ExistsAsync<User>(x => x.Username.Equals(username) && x.Id != _applicationUserService.Id);
        }
    }
}
