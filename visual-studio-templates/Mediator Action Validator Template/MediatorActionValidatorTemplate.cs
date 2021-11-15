using Axon.Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace $rootnamespace$
{

    public class $safeitemname$ : AbstractValidator<int>
    {
        private readonly IGenericRepository _repository;


        public $safeitemname$(IGenericRepository repository)
        {
            _repository = repository;

            RuleFor(x => x).NotEmpty().WithMessage("Property is required.")
                .Must(x => NotBeZero(x)).WithMessage("Invalid Property")
                .MustAsync(NotBeZeroAsync).WithMessage("Invalid Property Async");
        }


        public bool NotBeZero(int prop)
        {
            return prop != 0;
        }
        public async Task<bool> NotBeZeroAsync(int prop, CancellationToken cancellationToken)
        {
            return prop != 0;
        }
    }
}
