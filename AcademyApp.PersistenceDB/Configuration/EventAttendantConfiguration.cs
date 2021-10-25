using System;
using System.Collections.Generic;
using System.Text;
using AcademyApp.Domain.POCO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademyApp.PersistenceDB.Configuration
{
    public class EventAttendantConfiguration : IEntityTypeConfiguration<EventAttendant>
    {
        public void Configure(EntityTypeBuilder<EventAttendant> builder)
        {
            builder.ToTable("EventAttendants");

            builder.HasKey(x => new {x.AttendantId, x.EventId});

            builder.HasOne<Event>(ea => ea.Event)
                .WithMany(e => e.EventAttendant)
                .HasForeignKey(ea => ea.EventId);

            builder.HasOne<User>(ea => ea.Attendant)
                .WithMany(a => a.EventAttendant)
                .HasForeignKey(ea => ea.AttendantId);
        }
    }
}
