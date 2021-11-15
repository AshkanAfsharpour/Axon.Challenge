using Axon.Application.Common.Mappings;
using AutoMapper;
using Axon.Domain.Entities;
using System;

namespace Axon.Application.Users.ViewModels
{
    public class UserVM : IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserVM>();
        }
    }
}
