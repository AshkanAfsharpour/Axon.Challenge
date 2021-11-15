using System;
using System.Collections.Generic;
using System.Text;

namespace Axon.Application.GitHub.ViewModels
{
    public class GitHubRepositoryVM
    {
        public long GitId { get; set; }
        public int GitUserId { get; set; }
        public string NodeId { get; set; } 
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Homepage { get; set; }
        public string Language { get; set; }
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
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
