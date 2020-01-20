using System;

namespace Notifications.Common.Models
{
    /// <summary>
    /// Notification Event Model
    /// </summary>
    public class NotificationEventModel
    {
        /// <summary>
        /// Type 
        /// </summary>
        public string Type { get; set; }
        public NotificationData Data { get; set; }
        public Guid UserId { get; set; }
    }

    public enum EventType : int
    {
        AppointmentCancelled = 0
    }
}
