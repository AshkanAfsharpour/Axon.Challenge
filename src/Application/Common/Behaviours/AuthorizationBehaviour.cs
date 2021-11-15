using Axon.Application.Common.Interfaces;
using Axon.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IAxonContext _context;

        public AuthorizationBehaviour(
            IApplicationUserService applicationUserService,
            IAxonContext context)
        {
            _applicationUserService = applicationUserService;
            _context = context;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            if (_applicationUserService.IsAuthenticated)
            {
                var user = await _context.Users.Where(x => x.Id == _applicationUserService.Id).AsNoTracking().FirstOrDefaultAsync();

                if (user == null)
                throw new AuthorizationException("The Owner of this token Does not exist");

                if (user.SessionId != _applicationUserService.SessionId)  
                    throw new AuthorizationException("Invalid Session Please Login Again");
            }

            // authorization not required.
            return await next();
        }
    }
}
