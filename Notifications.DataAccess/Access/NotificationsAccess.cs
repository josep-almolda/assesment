using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Notifications.Common.Enums;
using Notifications.Common.Interfaces;
using Notifications.Common.Models;
using Notifications.DataAccess.Entities;

namespace Notifications.DataAccess.Access
{
    public class NotificationsAccess : INotificationsAccess
    {
        private readonly NotificationsDbContext dbContext;

        public NotificationsAccess(NotificationsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<NotificationModel> GetAllNotifications()
        {
            return dbContext.Notifications.Select(x => new NotificationModel()
            {
                Id = x.Id,
                Title = x.Title,
                UserId = x.UserId,
                Text = x.Text
            });
        }

        public TemplateModel GetTemplate(EventType type)
        {
            return dbContext.Templates
                .Where(x => x.EventType == type)
                .Select(x => new TemplateModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Body = x.Body
                })
                .FirstOrDefault();
        }

        public void AddNotification(NotificationModel notification)
        {
            dbContext.Notifications
                .Add(new NotificationEntity(notification.UserId, notification.Title, notification.Text));
        }
    }
}
