using Axon.Application.GitHub.Queries.GetGitHubProfileByUsername;
using Axon.Application.GitHub.Queries.GetMyGitHubProfile;
using Axon.Application.GitHub.Queries.GetMyGitHubRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axon.WebApi.Controllers
{
    public class GitHubController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> MyProfile([FromHeader] string accessToken) => Ok(await Mediator.Send(new GetMyGitHubProfileQuery { AccessToken = accessToken }));

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> MyRepositories([FromHeader] string accessToken) => Ok(await Mediator.Send(new GetMyGitHubRepositoriesQuery { AccessToken = accessToken }));

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Profile([FromHeader] string username) => Ok(await Mediator.Send(new GetGitHubProfileByUsernameQuery{ Username = username}));
    }
}
