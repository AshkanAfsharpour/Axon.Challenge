using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Axon.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Axon.Application.Common.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly IApplicationUserService _applicationUserService;

        public RequestLogger(ILogger<TRequest> logger, IApplicationUserService applicationUserService)
        {
            _logger = logger;
            _applicationUserService = applicationUserService;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            _logger.LogInformation("Axon Request: {Name} {@UserId} {@Request}", 
                name, _applicationUserService.Id, Convert.ToString(request));

            return Task.CompletedTask;
        }
    }
}
