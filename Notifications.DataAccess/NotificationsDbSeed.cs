using Microsoft.EntityFrameworkCore;
using Notifications.Common.Models;
using Notifications.DataAccess.Entities;
using System;
using System.Linq;

namespace Notifications.DataAccess
{
    public static class NotificationsDbSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var userId = Guid.NewGuid();
            modelBuilder.Entity<TemplateEntity>().HasData(new TemplateEntity()
            {
                Title = "Appointment Cancelled",
                Id = Guid.NewGuid(),
                EventType = "AppointmentCancelled",
                Body = "Hi {Firstname}, your appointment with {OrganisationName} at {AppointmentDateTime} has been cancelled for the following reason: {Reason}."
                
            });

            modelBuilder.Entity<NotificationEntity>().HasData(new NotificationEntity()
            {
                UserId = userId,
                DateAdded = DateTime.Now.AddDays(-2),
                EventType = EventType.AppointmentCancelled.ToString(),
                Message = "1 Hi Firstname, your appointment with OrganisationName at AppointmentDateTime has been cancelled for the following reason: Reason.",
                Id = Guid.NewGuid(),

            });

            modelBuilder.Entity<NotificationEntity>().HasData(new NotificationEntity()
            {
                UserId = userId,
                DateAdded = DateTime.Now.AddDays(-1),
                EventType = EventType.AppointmentCancelled.ToString(),
                Message = "2 Hi Firstname, your appointment with OrganisationName at AppointmentDateTime has been cancelled for the following reason: Reason.",
                Id = Guid.NewGuid(),
            });

            modelBuilder.Entity<NotificationEntity>().HasData(new NotificationEntity()
            {
                UserId = userId,
                DateAdded = DateTime.Now,
                EventType = EventType.AppointmentCancelled.ToString(),
                Message = "3 Hi Firstname, your appointment with OrganisationName at AppointmentDateTime has been cancelled for the following reason: Reason.",
                Id = Guid.NewGuid(),
            });

        }
    }

}
