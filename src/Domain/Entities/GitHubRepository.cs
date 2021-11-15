using Axon.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Axon.Domain.Entities
{
    public class GitHubRepository : AuditableEntity
    {
        public Guid GitUserId { get; set; }
        public long GitId { get; set; }
        public string NodeId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string? Description { get; set; }
        public string? Homepage { get; set; }
        public string? Language { get; set; }
        public string DefaultBranch { get; set; }
        public bool Private { get; set; }
        public long Size { get; set; }
        public int WatchersCount { get; set; }
        public int StargazersCount { get; set; }
        public int ForksCount { get; set; }
        public string Url { get; set; }
        public string HtmlUrl { get; set; }
        public string CloneUrl { get; set; }
        public string GitUrl { get; set; }
        public string SshUrl { get; set; }
        public DateTime? PushedAt { get; set; }

        public virtual GitHubProfile GitUser { get; set; }
    }
}
