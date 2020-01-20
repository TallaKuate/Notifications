using System;
using System.Collections.Generic;
using System.Text;

namespace Notifications.Common.Models
{
    public class NotificationModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public string EventType { get; set; }
    }
}
