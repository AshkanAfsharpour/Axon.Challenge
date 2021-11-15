using Axon.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.Common.Behaviours
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly IApplicationUserService _applicationUserService;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger, IApplicationUserService applicationUserService)
        {
            _timer = new Stopwatch();

            _logger = logger;
            _applicationUserService = applicationUserService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                var name = typeof(TRequest).Name;

                _logger.LogWarning("Axon Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}", 
                    name, _timer.ElapsedMilliseconds, _applicationUserService.Id, request);
            }

            return response;
        }
    }
}
