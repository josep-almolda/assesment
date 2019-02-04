using System;
using Notifications.DataAccess;
using Notifications.DataAccess.Entities;

namespace Web.Api.IntegrationTests
{
    public static class SeedData
    {
        public static void PopulateTestData(NotificationsDbContext dbContext)
        {
            var userGuid = Guid.Parse("b8412641-6436-49cc-816b-49b4a4f4ecd3");
            dbContext.Notifications.Add(new NotificationEntity(userGuid, "title1", "text1"));
            dbContext.Notifications.Add(new NotificationEntity(userGuid, "title2", "text3"));
            dbContext.Templates.Add(new TemplateEntity
            {
                Body = "Hi {Firstname}, your appointment with {OrganisationName} at {AppointmentDateTime} has been - cancelled for the following reason: {Reason}.",
                Title = "Appointment Cancelled"
            });
            dbContext.SaveChanges();
        }
    }
}
 