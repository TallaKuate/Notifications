using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notifications.Common.Interfaces;
using Notifications.Common.Models;
using Notifications.DataAccess.Entities;

namespace Notifications.DataAccess.Access
{
    public class NotificationsAccess : INotificationsAccess
    {
        private readonly NotificationsDbContext dbContext;

        public NotificationsAccess(NotificationsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<NotificationModel> GetAllNotifications()
        {
            return dbContext.Notifications.Select(x => new NotificationModel()
            {
                Id = x.Id,
            });
        }
        public async Task<IEnumerable<NotificationModel>> GetUserCancelledAppointmentNotifications(Guid userId)
        { 
            var result = await Task.Run(() =>
            {
                return dbContext.Notifications.Where(x => x.UserId == userId
                && x.EventType == EventType.AppointmentCancelled.ToString());
            }).ConfigureAwait(false);

            if (result == null || !result.Any())
                return null;

            return result.Select(x => new NotificationModel()
            {
                Id = x.Id,
                Message = x.Message,
                EventType = x.EventType,
                UserId = x.UserId
            });
        }

        public async Task<NotificationModel> AddUserNotification(NotificationEventModel notication)
        {
            var template = dbContext.Templates.FirstOrDefault(x => x.EventType == notication.Type);

            if (template == null)
                return null;
            
            var entity = new NotificationEntity
            { 
                UserId = notication.UserId,
                DateAdded = DateTime.Now, 
                EventType = notication.Type
            };

            entity.Message = template.Body;

            entity.Message = entity.Message.Replace("{Firstname}", notication.Data.FirstName);
            entity.Message = entity.Message.Replace("{OrganisationName}", notication.Data.OrganisationName);
            entity.Message = entity.Message.Replace("{AppointmentDateTime}", notication.Data.AppointmentDateTime);
            entity.Message = entity.Message.Replace("{Reason}", notication.Data.Reason);

            await dbContext.Notifications.AddAsync(entity).ConfigureAwait(false);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return new NotificationModel()
            {
                Id = entity.Id,
                Message = entity.Message,
                EventType = entity.EventType,
                UserId = entity.UserId
            };
        }

        public async Task<NotificationModel> GetLatestCancelledAppointmentNotification(Guid latestNotificationId)
        {
            var lastReceivedNotification = await Task.Run(() =>
            {
                return dbContext.Notifications.FirstOrDefault(x => x.Id == latestNotificationId);
            }).ConfigureAwait(false);

            if (lastReceivedNotification == null)
            {
                return null;
            }

            var newNotification = await Task.Run(() =>
            {
                return dbContext.Notifications.Where(x => x.UserId == lastReceivedNotification.UserId
                && x.DateAdded > lastReceivedNotification.DateAdded
                && x.EventType == EventType.AppointmentCancelled.ToString());
            }).ConfigureAwait(false);

            if (newNotification == null || !newNotification.Any())
            {
                return null;
            }

            var result = newNotification.OrderBy(x => x.DateAdded).First();
            return  new NotificationModel()
            {
                Id = result.Id,
                Message = result.Message,
                UserId= result.UserId, 
                EventType = result.EventType
            };
        }        
    }
}
