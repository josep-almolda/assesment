using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Notifications.Common.Enums;
using Notifications.DataAccess.Entities;

namespace Notifications.DataAccess
{
    public class NotificationsDbContext : DbContext
    {
        public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options)
            : base(options)
        { }

        public DbSet<NotificationEntity> Notifications { get; set; }
        public DbSet<TemplateEntity> Templates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TemplateEntity>()
                .Property(e => e.EventType)
                .HasConversion(
                    value => value.ToString(),
                    value => (EventType) Enum.Parse(typeof(EventType), value));

            modelBuilder.Entity<TemplateEntity>()
                .HasData(new TemplateEntity
                {
                    Id = Guid.Parse("d92af9fe-a1e7-4534-938c-5b3067edf3e0"),
                    EventType = EventType.AppointmentCancelled,
                    Body = "Hi {Firstname}, your appointment with {OrganisationName} at {AppointmentDateTime} has been - cancelled for the following reason: {Reason}.",
                    Title = "Appointment Cancelled"
                });
            modelBuilder.Entity<NotificationEntity>();
        }
    }
}
