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

            await connection.StartAsync();
            Person per = new Person
            {
                num = 33.2,
                size = 11.55
            };
            while (true)
            {
                Delay(1000);
                await connection.SendAsync("SendMessage", per);
            }
        }

        private static bool Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) >= milliSecond) ; //毫秒
            return true;
        }
    }
}