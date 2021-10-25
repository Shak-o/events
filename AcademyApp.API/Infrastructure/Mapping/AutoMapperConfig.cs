using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyApp.API.Infrastructure.Models;
using AcademyApp.Domain.POCO;
using AcademyApp.Service.Implementation;
using AcademyApp.Service.Models;
using AutoMapper;

namespace AcademyApp.API.Infrastructure.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Event, EventServiceModel>().ForMember(dest => dest.Users, opt => opt.MapFrom<EventCustomResolver>());
            CreateMap<User, AttendantServiceModel>();
            CreateMap<User, UserServiceModel>();

            CreateMap<EventServiceModel, Event>().ForMember(dest => dest.EventAttendant, opt => opt.MapFrom<ReverseEventResolver>());
            CreateMap<AttendantServiceModel, User>();
            CreateMap<UserServiceModel, User>();

            CreateMap<EventGetModel, EventServiceModel>();
            CreateMap<EventPostModel, EventServiceModel>();
            CreateMap<EventPutModel, EventServiceModel>();
            CreateMap<UserModel, UserServiceModel>();
            CreateMap<AttendantModel, AttendantServiceModel>();
            CreateMap<Archive, ArchiveServiceModel>();
            CreateMap<ArchiveServiceModel, Archive>();
            CreateMap<ArchivePostModel, ArchiveServiceModel>();
        }
    }
}
