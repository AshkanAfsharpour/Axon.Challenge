using AutoMapper;
using Axon.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $rootnamespace$
{
    public class $safeitemname$ : IMapFrom<int>
    {

        public void Mapping(Profile profile)
        {
            profile.CreateMap<int, $safeitemname$>();
        }
    }
}
