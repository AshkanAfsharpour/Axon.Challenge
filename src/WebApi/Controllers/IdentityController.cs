using Axon.Application.Identity.Commands.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Axon.WebApi.Controllers
{
    public class IdentityController : BaseController
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViaUsernamePasswordCommand command) => Ok(await Mediator.Send(command));
    }
}
