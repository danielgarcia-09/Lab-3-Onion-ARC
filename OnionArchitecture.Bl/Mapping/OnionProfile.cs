using AutoMapper;
using OnionArchitecture.Bl.Dto;
using OnionArchitecture.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Bl.Mapping
{
    public class OnionProfile : Profile
    {
        public OnionProfile()
        {
            CreateMap<EmployeeDto, Employee>()
                .ReverseMap();

            CreateMap<EmployeeDto, Employee>()
               .ForMember(dest => dest.Id, option => option.Ignore());

            CreateMap<BossDto, Boss>()
                .ReverseMap();

            CreateMap<BossDto, Boss>()
               .ForMember(dest => dest.Id, option => option.Ignore());
        }
    }
}
