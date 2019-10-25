using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Transavia.Core.Entities;
using Transavia.Web.Models;

namespace Transavia.Web.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<LoginModel, User>();
            CreateMap<RegisterModel, User>();
            CreateMap<User, LoginModel>();
            CreateMap<User, RegisterModel>();
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
        }
    }
}
