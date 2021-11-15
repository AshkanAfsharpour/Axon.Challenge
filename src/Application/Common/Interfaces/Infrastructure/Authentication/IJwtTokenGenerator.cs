using System;

namespace Axon.Application.Common.Interfaces
{
    public interface IjwtServices
    {
        string GenerateRefreshToken();
        string IssueToken(Guid identifier, int roleId, Guid sessionId);

    }
}