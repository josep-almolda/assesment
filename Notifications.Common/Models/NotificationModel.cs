﻿using System;
using Notifications.Common.Enums;

namespace Notifications.Common.Models
{
    public class NotificationModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
