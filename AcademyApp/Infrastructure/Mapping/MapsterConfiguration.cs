using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyApp.Models;
using AcademyApp.Models.Account;
using AcademyApp.Models.Event;
using Client.Models;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace AcademyApp.Infrastructure.Mapping
{
    public static class MapsterConfiguration
    {
        public static void AddMapping(this IServiceCollection services)
        {
            TypeAdapterConfig<RegistrationViewModel, UserIdentity>.NewConfig();
            TypeAdapterConfig<UpdateViewModel, UserIdentity>.NewConfig().TwoWays();
            TypeAdapterConfig<UserIdentity, UserManageViewModel>.NewConfig();
            TypeAdapterConfig<EventModel, EventViewModel>.NewConfig().TwoWays();
            TypeAdapterConfig<Credentials, LoginViewModel>.NewConfig().TwoWays();
        }
    }
}
