using System;
using System.Collections.Generic;
using System.Text;
using AcademyApp.Data.EF;
using AcademyApp.Service.Implementation;
using AcademyApp.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AcademyApp.Service.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {

            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IArchiveService, ArchiveService>();
            services.AddRepositories();
        }
    }
}
