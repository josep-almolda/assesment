using System;
using Notifications.Common.Enums;

namespace Notifications.Common.Models
{
    public class EventModel
    {
        public EventType Type { get; set; }
        public EventData Data { get; set; }
        public Guid UserId { get; set; }
    }
}
