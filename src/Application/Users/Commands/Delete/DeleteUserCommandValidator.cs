using Axon.Application.Common.Interfaces;
using Axon.Domain.Entities;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        private readonly IGenericRepository _repository;


        public DeleteUserCommandValidator(IGenericRepository repository)
        {
            this._repository = repository;


            RuleFor(x => x.Id).NotEmpty().WithMessage("User Id is required.")
                .MustAsync(beValidUserId).WithMessage("No User Found with this Id");
        }

        public async Task<bool> beValidUserId(Guid id, CancellationToken cancellationToken)
        {
            return !await _repository.ExistsAsync<User>(x => x.Id == id);

        }

    }
}
