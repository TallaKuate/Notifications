using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;

namespace Notification.Tests.SocketClient
{
    class Program
    {
        static HubConnection connection;
        //Set your last notification here 
        static string messageId = "5F46E0CD-C3EF-4D81-8646-0B612EE93F99";
        static void Main(string[] args)
        {
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44309/NewNotification")
                .WithAutomaticReconnect()
                .Build();

            //Start the connection
            var t = connection.StartAsync();          
            t.Wait();
            System.Console.WriteLine("Connected , current messages Id " + messageId);

            connection.On<string, string>("ReceiveMessage", (Id, Message) =>
            {
                System.Console.WriteLine("New Notificatication ----> " +Message);
                System.Console.WriteLine( "New Message-Id  ----> "+Id);
            });

            var cts = new CancellationTokenSource();
            var t2 =connection.InvokeAsync("SendMessage", messageId);
            if (!t2.Wait(5000, cts.Token)) cts.Cancel();

        }

    }
}
