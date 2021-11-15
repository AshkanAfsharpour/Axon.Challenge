using Axon.Application.Common.Mappings;
using AutoMapper;
using Axon.Domain.Entities;
using System;

namespace Axon.Application.Identity.ViewModels
{
    public class LoginVM : IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string RefreshToken { get; set; }
        public string Token { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, LoginVM>()
                .IgnoreAllNonExisting();
        }
    }
}
