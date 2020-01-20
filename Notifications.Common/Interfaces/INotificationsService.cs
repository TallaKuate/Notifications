using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Notifications.Common.Models;

namespace Notifications.Common.Interfaces
{
    public interface INotificationsService
    {
        IReadOnlyCollection<NotificationModel> GetAllNotifications();
        Task<IReadOnlyCollection<NotificationModel>> GetUserCancelledAppointmentNotifications(Guid userId);
        Task<NotificationModel> AddUserNotification(NotificationEventModel notification);
        Task<NotificationModel> GetLatestCancelledAppointmentNotification(Guid latestNotification);
    }
}
