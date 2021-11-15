using FluentValidation;
using Axon.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.GitHub.Commands.UpdateGitHubProfile
{
    public class UpdateGitHubProfileCommandValidator : AbstractValidator<UpdateGitHubProfileCommand>
    {
        private readonly IGenericRepository _repository;

        public UpdateGitHubProfileCommandValidator(IGenericRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.GitHubProfile).NotEmpty().WithMessage("Property is required.");

        }
    }
}
