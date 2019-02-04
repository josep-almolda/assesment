using System;
using Notifications.Common.Enums;
using Notifications.Common.Models;

namespace Notifications.DataAccess.Entities
{
    public class NotificationEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
