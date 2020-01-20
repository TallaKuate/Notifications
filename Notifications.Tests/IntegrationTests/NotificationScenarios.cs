using Newtonsoft.Json;
using Notifications.Common.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Notifications.Tests.IntegrationTests
{
    public class NotificationScenarios
     : NotificationScenariosBase
    {
        string userId = "AE60BBBA-AC45-41E8-BB93-2E3DB395B114";

        [Fact]
        public async Task Get_Notification_by_userid_and_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(HttPActionRoute.GetUserNotification(userId));
                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        public async Task Get_Notification_by_userid_and_response_Not_Found_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(HttPActionRoute.GetUserNotification((Guid.NewGuid()).ToString()));

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async Task Add_Notification_by_userid_and_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var notificationEventModel = new NotificationEventModel { 
                
                   Type = EventType.AppointmentCancelled.ToString(),
                   UserId = new Guid(userId), 
                   Data = new NotificationData
                   {
                       AppointmentDateTime = "28/01/2020",
                       FirstName = "Test",
                       OrganisationName = "Integration",
                       Reason = "Robustness"
                   }
                };

                var content = new StringContent(JsonConvert.SerializeObject(notificationEventModel), 
                    Encoding.UTF8, "application/json");
                var response = await server.CreateClient()
                    .PostAsync(HttPActionRoute.AddUserNotification(), content);

                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        public async Task Add_Notification_by_userid_and_response_ok_Not_Found()
        {
            using (var server = CreateServer())
            {
                var notificationEventModel = new NotificationEventModel();

                var content = new StringContent(JsonConvert.SerializeObject(notificationEventModel),
                     Encoding.UTF8, "application/json");
                var response = await server.CreateClient()
                    .PostAsync(HttPActionRoute.AddUserNotification(), content);

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }

}
