using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyApp.Domain.POCO;
using AcademyApp.Service.Models;
using AutoMapper;

namespace AcademyApp.API.Infrastructure.Mapping
{
    public class ReverseEventResolver : IValueResolver<EventServiceModel,Event, List<EventAttendant>>
    {
        private readonly IMapper _mapper;
        public ReverseEventResolver(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<EventAttendant> Resolve(EventServiceModel source, Event destination, List<EventAttendant> destMember, ResolutionContext context)
        {
            List<EventAttendant> list = new List<EventAttendant>();
            if (source.Users != null)
            {
                foreach (var attendant in source.Users)
                {
                    var convertUser = _mapper.Map<AttendantServiceModel, User>(attendant);
                    var convert = new EventAttendant
                    {
                        Attendant = convertUser, Event = destination, AttendantId = attendant.Id,
                        EventId = destination.Id
                    };
                    list.Add(convert);
                }
            }

            return list;
        }
    }
}
