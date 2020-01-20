using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notifications.Common.Interfaces;
using Notifications.Common.Models;

namespace Notifications.Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly INotificationsAccess notificationsAccess;

        public NotificationsService(INotificationsAccess notificationsAccess)
        {
            this.notificationsAccess = notificationsAccess;
        }

        public IReadOnlyCollection<NotificationModel> GetAllNotifications()
        {
            return this.notificationsAccess.GetAllNotifications().ToList();
        }

        public async Task<IReadOnlyCollection<NotificationModel>> GetUserCancelledAppointmentNotifications(Guid userId)
        {
            var result = await  this.notificationsAccess.GetUserCancelledAppointmentNotifications(userId).ConfigureAwait(false);
            if (result == null || !result.Any())
                return null;
            return result.ToList();
        }

        public async Task<NotificationModel> AddUserNotification(NotificationEventModel notification)
        {
            var result = await this.notificationsAccess.AddUserNotification(notification).ConfigureAwait(false);        
            return result;
        }
        public async Task<NotificationModel> GetLatestCancelledAppointmentNotification(Guid latestNotification) 
        {
            var result = await this.notificationsAccess.GetLatestCancelledAppointmentNotification(latestNotification).ConfigureAwait(false);        
            return result;
        }
    }
}
