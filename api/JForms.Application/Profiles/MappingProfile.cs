using AutoMapper;
using JForms.Data.Dto.Form;
using JForms.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<CreateFormDto, Form>();
        }
    }
}
