using System;
using System.Collections.Generic;
using System.Text;
using AcademyApp.Data.EF.Implementation;
using AcademyApp.Data.EF.Interface;
using AcademyApp.Domain.POCO;
using AcademyApp.PersistenceDB.Context;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.Tests
{
    public static class MakeNewClasses
    {
        public static EventContext GetContext()
        {
            return new EventContext(new DbContextOptions<EventContext>());
        }
        public static BaseRepository<Event> GetEventRepo(EventContext context)
        {
            var test = new BaseRepository<Event>(context);
            return test;
        }
        public static BaseRepository<User> GetUserRepository(EventContext context)
        {
            var test = new BaseRepository<User>(context);
            return test;
        }

    }
}
