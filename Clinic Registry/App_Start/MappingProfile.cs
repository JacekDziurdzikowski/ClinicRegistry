using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Clinic_Registry.Models;

namespace Clinic_Registry.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Patient, Patient>();

        }
    }
}