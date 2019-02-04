using System;
using System.Collections.Generic;
using System.Text;
using Notifications.Common.Enums;

namespace Notifications.Common.Models
{
    public class NotificationModel
    {
        public Guid Id { get; set; }
        public EventType? Type { get; set; }
        public EventData Data { get; set; }
        public Guid UserId { get; set; }
    }
}
