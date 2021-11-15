using Axon.Domain.Common;
using System;

#nullable disable

namespace Axon.Domain.Entities
{
    public partial class User : AuditableEntity
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public bool IsSuspended { get; set; }
        public Guid SessionId { get; set; }
    }
}
