using Axon.Application.Users.Commands.CreateUser;
using Axon.Application.Users.Commands.UpdateUser;
using Axon.Application.Users.Commands.DeleteUser;
using Axon.Application.Users.Queries.GetDetail;
using Axon.Application.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Axon.WebApi.Controllers
{
    public class UsersController : BaseController
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command) =>  Ok(await Mediator.Send(command));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command) => Ok(await Mediator.Send(command));

        [Authorize(Policy = "CanPurge")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] DeleteUserCommand command) => Ok(await Mediator.Send(command));

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUserDetailQuery query) => Ok(await Mediator.Send(query));

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllUsersQuery query) => Ok(await Mediator.Send(query));

    }
}
