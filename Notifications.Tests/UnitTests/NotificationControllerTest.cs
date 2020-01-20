using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notifications.Common.Models;
using Notifications.Controllers;
using Notifications.DataAccess;
using Notifications.DataAccess.Access;
using Notifications.DataAccess.Entities;
using Notifications.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Notifications.Tests.UnitTests
{
    
    public class NotificationControllerTest
    {
        private readonly DbContextOptions<NotificationsDbContext> _dbOptions;
       Guid testUserId;
        public NotificationControllerTest()
        {
            _dbOptions = new DbContextOptionsBuilder<NotificationsDbContext>()
                .UseInMemoryDatabase(databaseName: "in-memory")
                .Options;

            using (var dbContext = new NotificationsDbContext(_dbOptions))
            {
                dbContext.AddRange(GetNoticationSeed());
                dbContext.Add(GetTemplateData());
                dbContext.SaveChanges();
            }
        }

        [Fact]
        public async Task Get_user_Notification_success()
        {
            //Arrange
            var userId = testUserId;

            //Act
            var catalogContext = new NotificationsDbContext(_dbOptions);
            var accessServ = new NotificationsAccess(catalogContext);
            var NotificationServ = new NotificationsService(accessServ);
            var testController = new NotificationsController(NotificationServ);
            var actionResult = await testController.GetUserCancelledAppointmentNotification(userId).ConfigureAwait(false);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
            var result = Assert.IsAssignableFrom<List<NotificationModel>>(((OkObjectResult)actionResult).Value);
            Assert.Equal(3, result.Count);
            Assert.Equal(EventType.AppointmentCancelled.ToString(), result[0].EventType);
            Assert.Contains("Hi Firstname, your appointment with OrganisationName at AppointmentDateTime has been cancelled for the following reason: Reason.",
           result[0].Message);
        }

        [Fact]
        public async Task Get_user_Notification_Failed()
        {
            //Arrange
            var userId = new Guid();

            //Act
            var catalogContext = new NotificationsDbContext(_dbOptions);
            var accessServ = new NotificationsAccess(catalogContext);
            var NotificationServ = new NotificationsService(accessServ);
            var testController = new NotificationsController(NotificationServ);
            var actionResult = await testController.GetUserCancelledAppointmentNotification(userId).ConfigureAwait(false);

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult);
            var result = ((NotFoundObjectResult)actionResult).Value;
            Assert.Null(result);           
        }

        [Fact]
        public async Task Add_user_Notification_success()
        {
            //Arrange
            var model = new NotificationEventModel
            {
                Data = new NotificationData
                {
                    AppointmentDateTime = "23/01/2020",
                    FirstName = "Romio",
                    OrganisationName = "Little Runners",
                    Reason = "Healthy"
                },

                Type = "AppointmentCancelled",
                UserId = Guid.NewGuid()           
            };

            //Act
            var catalogContext = new NotificationsDbContext(_dbOptions);
            var accessServ = new NotificationsAccess(catalogContext);
            var NotificationServ = new NotificationsService(accessServ);
            var testController = new NotificationsController(NotificationServ);
            var actionResult = await testController.AddUserNotification(model).ConfigureAwait(false);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
            var result = Assert.IsAssignableFrom<NotificationModel>(((OkObjectResult)actionResult).Value);
            Assert.Contains("Healthy", result.Message);
        }

        TemplateEntity GetTemplateData()
        {
            return new TemplateEntity
            {
                Title = "Appointment Cancelled",
                Id = Guid.NewGuid(),
                EventType = "AppointmentCancelled",
                Body = "Hi {Firstname}, your appointment with {OrganisationName} at {AppointmentDateTime} has been cancelled for the following reason: {Reason}."


            };
        }

        List <NotificationEntity> GetNoticationSeed()
        {

            testUserId = Guid.NewGuid();
            var n1 = new NotificationEntity()
            {
                UserId = testUserId,
                DateAdded = DateTime.Now.AddDays(-2),
                EventType = EventType.AppointmentCancelled.ToString(),
                Message = "1 Hi Firstname, your appointment with OrganisationName at AppointmentDateTime has been cancelled for the following reason: Reason.",
                Id = Guid.NewGuid(),

            };

            var n2 = new NotificationEntity()
            {
                UserId = testUserId,
                DateAdded = DateTime.Now.AddDays(-1),
                EventType = EventType.AppointmentCancelled.ToString(),
                Message = "2 Hi Firstname, your appointment with OrganisationName at AppointmentDateTime has been cancelled for the following reason: Reason.",
                Id = Guid.NewGuid(),
            };

            var n3 = new NotificationEntity()
            {
                UserId = testUserId,
                DateAdded = DateTime.Now,
                EventType = EventType.AppointmentCancelled.ToString(),
                Message = "3 Hi Firstname, your appointment with OrganisationName at AppointmentDateTime has been cancelled for the following reason: Reason.",
                Id = Guid.NewGuid(),
            };

            return new List<NotificationEntity>
            {
                n1, n2, n3
            };
        }

    }


}
