using System;
using Notifications.Common.Models;

namespace Notifications.DataAccess.Entities
{
    public class NotificationEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string EventType { get; set; }
        public string Message { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
