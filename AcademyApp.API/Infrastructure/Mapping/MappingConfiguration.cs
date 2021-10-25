using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyApp.Domain.POCO;
using AcademyApp.Service.Models;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace AcademyApp.API.Infrastructure.Mapping
{
    public static class MappingConfiguration
    {
        public static void AddMapping(this IServiceCollection services)
        {
            //
            // TypeAdapterConfig<UserServiceModel, User>
            //     .NewConfig()
            //     .Map(dest => dest.EventAttendant, src => src.Events != null ? src.Events.Select(x => new EventAttendant { Event = x.Adapt<Event>(), AttendantId = src.Id, EventId = x.Id }) : default);

            TypeAdapterConfig<EventServiceModel, Event>.NewConfig().
                Map(dest => dest.EventAttendant, src => src.Users != null ? src.Users.Select(e => new EventAttendant {Event = src.Adapt<Event>(), AttendantId = e.Id, EventId =  src.Id}) : default );
            //
            // TypeAdapterConfig<User, UserServiceModel>.NewConfig()
            //     .Map(dest => dest.Events, src => src.EventAttendant.Select(x => x.Event));

            TypeAdapterConfig<Event, EventServiceModel>.NewConfig()
                .Map(dest => dest.Users, src => src.EventAttendant.Select(x => x.Attendant));
            // TypeAdapterConfig<UserServiceModel, User>.NewConfig().TwoWays().Ignore(x => x.Event);
            // TypeAdapterConfig<UserServiceModel, User>.NewConfig().TwoWays().Ignore(x => x.EventAttendant);
            // TypeAdapterConfig<User, UserServiceModel>.NewConfig().TwoWays().Ignore(x => x.Event);
            // TypeAdapterConfig<User, UserServiceModel>.NewConfig().TwoWays().Ignore(x => x.Events);
            TypeAdapterConfig<Event, EventServiceModel>.NewConfig().Ignore(x => x.User);

        }
    }
}
