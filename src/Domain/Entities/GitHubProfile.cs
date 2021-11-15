using Axon.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Axon.Domain.Entities
{
    public class GitHubProfile : AuditableEntity
    {
        public GitHubProfile()
        {
            GitHubRepositories = new HashSet<GitHubRepository>();
        }

        public int GitId { get; set; }
        public string NodeId { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string? Email { get; set; }
        public string? Blog { get; set; }
        public string? Bio { get; set; }
        public string? Location { get; set; }
        public string AvatarUrl { get; set; }
        public string Url { get; set; }
        public string HtmlUrl { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public int? Collaborators { get; set; }
        public int? DiskUsage { get; set; }
        public string? Company { get; set; }
        public bool Suspended { get; set; }
        public DateTime? SuspendedAt { get; set; }

        public virtual ICollection<GitHubRepository> GitHubRepositories { get; set; }
    }
}
