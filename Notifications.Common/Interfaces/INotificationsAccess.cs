using System;
using System.Collections.Generic;
using System.Text;
using Notifications.Common.Enums;
using Notifications.Common.Models;

namespace Notifications.Common.Interfaces
{
    public interface INotificationsAccess
    {
        IEnumerable<NotificationModel> GetAllNotifications();
        TemplateModel GetTemplate(EventType type);
        void AddNotification(NotificationModel notification);
        IEnumerable<NotificationModel> GetNotificationsById(Guid userId);
    }
}
