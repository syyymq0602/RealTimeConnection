using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using SingnalRClient.Users;

namespace SingnalRClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/Chat")
                .Build();
            
            connection.On<Person>("ReceiveMessage", pers =>
            {
                Console.WriteLine(pers.num);
            });
            
            connection.On<string, string>("ReceiveMessageBy", (user, message) =>
            {
                Console.WriteLine(user);
            });
            
            Person per = new Person
            {
                num = 33.2,
                size = 11.55
            };
            await connection.StartAsync();
            while (true)
            {
                DelaySeconds(1);
                await connection.SendAsync("SendMessage", per);
            }
        }

        private static bool DelaySeconds(int second)
        {
            DateTime now = DateTime.Now;
            int s;
            do
            {
                TimeSpan spand = DateTime.Now - now;
                s = spand.Seconds;
            }
            while (s < second);
            return true;
        }
    }
}