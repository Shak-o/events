using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Models;
using EventsApp.Models;
using EventsApp.Models.Account;
using EventsApp.Models.Event;
using EventsApp.Models.Event.Requests;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace EventsApp.Infrastructure.Mapping
{
    public static class MapsterConfiguration
    {
        public static void AddMapping(this IServiceCollection services)
        {
            TypeAdapterConfig<RegistrationViewModel, UserIdentity>.NewConfig();
            TypeAdapterConfig<EventViewModel, UpdateRequest>.NewConfig();
            TypeAdapterConfig<EventModel, EventViewModel>.NewConfig().TwoWays();
            TypeAdapterConfig<EventModel, UpdateRequest>.NewConfig().TwoWays();
            TypeAdapterConfig<LoginViewModel, Credentials>.NewConfig().TwoWays();
        }
    }
}
