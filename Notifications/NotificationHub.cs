using Microsoft.AspNetCore.SignalR;
using Notifications.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notifications
{
    public class NotificationHub :Hub
    {
        private readonly INotificationsService _notificationsService;
        public NotificationHub(INotificationsService notificationsService)
        {
            _notificationsService = notificationsService; 
        }

        public async Task SendMessage(string latestNotificationId)
        {
            var notificationModel = await _notificationsService
                .GetLatestCancelledAppointmentNotification(new Guid(
                latestNotificationId) );

            await Clients.Caller.SendAsync(
                "ReceiveMessage",
                notificationModel.Id,
                notificationModel.Message);
        }
    }
}
