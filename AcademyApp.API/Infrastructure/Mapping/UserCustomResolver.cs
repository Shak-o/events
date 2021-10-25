using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyApp.Domain.POCO;
using AcademyApp.Service.Models;
using AutoMapper;

namespace AcademyApp.API.Infrastructure.Mapping
{
    public class UserCustomResolver : IValueResolver<User, UserServiceModel, List<EventServiceModel>>
    {
        private readonly IMapper _mapper;
        public UserCustomResolver(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<EventServiceModel> Resolve(User source, UserServiceModel destination, List<EventServiceModel> destMember, ResolutionContext context)
        {
            List<EventServiceModel> list = new List<EventServiceModel>();

            foreach (var item in source.EventAttendant)
            {
                var convert = _mapper.Map<Event, EventServiceModel>(item?.Event);
                list.Add(convert);
            }

            return list;
        }
    }
}
