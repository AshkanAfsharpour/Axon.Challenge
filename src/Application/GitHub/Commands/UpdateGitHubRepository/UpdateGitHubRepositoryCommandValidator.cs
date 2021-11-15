using FluentValidation;
using Axon.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.GitHub.Commands.UpdateGitHubRepository
{
    public class UpdateGitHubRepositoryCommandValidator : AbstractValidator<UpdateGitHubRepositoryCommand>
    {
        private readonly IGenericRepository _repository;

        public UpdateGitHubRepositoryCommandValidator(IGenericRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.GitHubRepository).NotEmpty().WithMessage("Property is required.");
        }
    }
}
