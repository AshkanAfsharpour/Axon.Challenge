using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Axon.Application.Common.Interfaces;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Linq;
using System;
using System.Collections.Generic;
using Axon.Application.Common.Extentions;

namespace Axon.WebApi.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public Guid? Id => (_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)).IsGuid() ?
              Guid.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)) : null;
        public string RoleTitle => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
        public string IP => _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        public bool IsAuthenticated => this.Id != default && this.Id != null;
        public Guid? SessionId => (_httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Jti)).IsGuid() ?
            Guid.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Jti)) : null;
    }
}
