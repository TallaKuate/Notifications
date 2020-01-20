
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notifications.DataAccess;
using System;
using System.IO;
using System.Reflection;

namespace Notifications.Tests.IntegrationTests
{
    public class NotificationScenariosBase
    {
        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(NotificationScenariosBase))
              .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))   
                //.UseEnvironment("Development")
                .UseStartup<Startup>();

            var testServer = new TestServer(hostBuilder);
            return testServer;
        }

        public static class HttPActionRoute
        {
            public static string GetUserNotification(string id)
            {
                return $"api/Notifications/user/{id}";
            }
            public static string AddUserNotification()
            {
                return $"api/Notifications";
            }
        }
    }

}
