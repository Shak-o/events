using System;
using System.Collections.Generic;
using System.Text;
using AcademyApp.Domain.POCO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AcademyApp.PersistenceDB.Context
{
    public class EventContext : DbContext
    {
        private readonly string _connectionString;

        public EventContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventContext).Assembly);
        }


        public EventContext(DbContextOptions<EventContext> options) : base(options)
        {

        }



        private DbSet<Event> Events { get; set; }
        private DbSet<Archive> Archives { get; set; }
    }
}
