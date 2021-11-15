using Axon.Application.Common.Interfaces;
using Axon.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace $rootnamespace$
{
    public class $safeitemname$ : IRequest<ServiceResponse<int>>
    {
    }

    public class $safeitemname$Handler : IRequestHandler<$safeitemname$, ServiceResponse<int>>
    {
        private readonly IGenericRepository _repository;

        public $safeitemname$Handler(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<int>> Handle($safeitemname$ request, CancellationToken cancellationToken)
        {
            
            return ServiceResponse.OK(1);

        }
    }
}
