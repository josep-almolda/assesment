using System;
using System.Collections.Generic;
using System.Text;
using Notifications.Common.Models;

namespace Notifications.Common.Interfaces
{
    public interface INotificationsService
    {
        IReadOnlyCollection<NotificationModel> GetAllNotifications();
        void CreateNotification(EventModel eventModel);
        IReadOnlyCollection<NotificationModel> GetNotificationsByUser(Guid userId);
    }
}
