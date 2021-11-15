using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axon.Application.IntegrationTests.Models
{
    public class MockApplicationUserService
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public bool IsAuthenticated { get; set; }
        public Guid SessionId { get; set; }

        public MockApplicationUserService()
        {
            Id = new Guid();
            Role = "Anonymous";
            IsAuthenticated = false;
            SessionId = new Guid();
        }
    
    }
}
