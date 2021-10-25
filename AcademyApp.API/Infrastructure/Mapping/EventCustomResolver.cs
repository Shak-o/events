using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyApp.Domain.POCO;
using AcademyApp.Service.Models;
using AutoMapper;

namespace AcademyApp.API.Infrastructure.Mapping
{
    public class EventCustomResolver : IValueResolver<Event, EventServiceModel, List<AttendantServiceModel>>
    {
        private readonly IMapper _mapper;
        public EventCustomResolver(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<AttendantServiceModel> Resolve(Event source, EventServiceModel destination, List<AttendantServiceModel> destMember, ResolutionContext context)
        {
            List<AttendantServiceModel> convertedList = new List<AttendantServiceModel>();

            foreach (var value in source.EventAttendant)
            {
                var convert = _mapper.Map<User, AttendantServiceModel>(value.Attendant);
                convertedList?.Add(convert);
            }

            return convertedList;
        }
    }
}
