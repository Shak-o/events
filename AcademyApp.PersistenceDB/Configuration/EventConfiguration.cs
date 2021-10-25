using System;
using System.Collections.Generic;
using System.Text;
using AcademyApp.Domain.POCO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademyApp.PersistenceDB.Configuration
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.Title).HasMaxLength(50);
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.ImgName);
            builder.Property(x => x.ImgPath);
            builder.Property(x => x.EditEndDate);
            builder.Property(x => x.EndDate);
            builder.Property(x => x.CreateDate);
            builder.Property(x => x.IsActive);

            builder.HasOne<User>(x => x.User).WithMany(x => x.Event);
        }
    }
}
