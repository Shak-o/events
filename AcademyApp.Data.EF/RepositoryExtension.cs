using System;
using System.Collections.Generic;
using System.Text;
using AcademyApp.Data.EF.Implementation;
using AcademyApp.Data.EF.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace AcademyApp.Data.EF
{
    public static class RepositoryExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IArchiveRepository, ArchiveRepository>();
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
