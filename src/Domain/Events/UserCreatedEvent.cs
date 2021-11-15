using Axon.Domain.Entities;
using Axon.Domain.Common;

namespace Axon.Domain.Events
{
    public class UserCreatedEvent : DomainEvent
    {
        public UserCreatedEvent(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}
