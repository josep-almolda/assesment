using System;
using System.Collections.Generic;
using System.Linq;
using Notifications.Common.Interfaces;
using Notifications.Common.Models;

namespace Notifications.Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly INotificationsAccess notificationsAccess;
        private readonly IBodyParser bodyParser;


        public NotificationsService(INotificationsAccess notificationsAccess, IBodyParser bodyParser)
        {
            this.notificationsAccess = notificationsAccess;
            this.bodyParser = bodyParser;
        }

        public IReadOnlyCollection<NotificationModel> GetAllNotifications()
        {
            return this.notificationsAccess.GetAllNotifications().ToList();
        }

        public void CreateNotification(EventModel eventModel)
        {
            var template = this.notificationsAccess.GetTemplate(eventModel.Type);
            var text = bodyParser.ParseEventBody(template.Body, eventModel.Data);
            this.notificationsAccess.AddNotification(new NotificationModel
            {
                Title = template.Title,
                UserId = eventModel.UserId,
                Text = text
            });
        }

        public IReadOnlyCollection<NotificationModel> GetNotificationsByUser(Guid userId)
        {
            return this.notificationsAccess.GetNotificationsById(userId).ToList();
        }
    }
}
