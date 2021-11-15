using Axon.Application.Common.Mappings;
using AutoMapper;
using Axon.Domain.Entities;
using System;
namespace Axon.Application.Users.ViewModels
{
    public class ProfileVM : IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Token { get; set; }
        public Guid SessionId { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, ProfileVM>()
                .IgnoreAllNonExisting();
        }
    }
}
