using System;

namespace Axon.Application.Common.Interfaces
{
    public interface IApplicationUserService
    {
        Guid? Id { get; }
        string RoleTitle { get; }
        string IP { get; }
        bool IsAuthenticated { get; }
        Guid? SessionId { get; }
    }
}
