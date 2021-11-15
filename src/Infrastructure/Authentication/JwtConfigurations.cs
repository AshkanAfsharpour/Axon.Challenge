using System.Collections.Generic;

namespace Axon.Infrastructure.Authentication
{
    public class JwtConfigurations
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public List<string> ValidIssuers { get; set; }
        public List<string> ValidAudiences { get; set; }
        public string Secret { get; set; }

    }
}