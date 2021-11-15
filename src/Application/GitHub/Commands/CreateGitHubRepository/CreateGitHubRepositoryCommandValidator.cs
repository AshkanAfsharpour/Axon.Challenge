using FluentValidation;
using Axon.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.GitHub.Commands.CreateGitHubRepository
{
    public class CreateGitHubRepositoryCommandValidator : AbstractValidator<CreateGitHubRepositoryCommand>
    {
        private readonly IGenericRepository _repository;

        public CreateGitHubRepositoryCommandValidator(IGenericRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.GitHubRepository).NotEmpty().WithMessage("Property is required.");
        }
    }
}
