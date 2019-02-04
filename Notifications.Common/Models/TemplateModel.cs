using System;
using System.Collections.Generic;
using System.Text;
using Notifications.Common.Enums;

namespace Notifications.Common.Models
{
    public class TemplateModel
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
    }
}
