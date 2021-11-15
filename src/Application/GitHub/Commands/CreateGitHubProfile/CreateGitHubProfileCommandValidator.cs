using FluentValidation;
using Axon.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.GitHub.Commands.CreateGitHubProfile
{
    public class CreateGitHubProfileCommandValidator : AbstractValidator<CreateGitHubProfileCommand>
    {
        private readonly IGenericRepository _repository;

        public CreateGitHubProfileCommandValidator(IGenericRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.GitHubProfile).NotEmpty().WithMessage("Property is required.");
        }
    }
}
