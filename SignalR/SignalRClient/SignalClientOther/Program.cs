using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace SignalClientOther
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/Chat")
                .Build();
            
            connection.On<string>("ReceiveMessage", str =>
            {
                var per = JsonConvert.DeserializeObject<Person>(str);
                Console.WriteLine(per.num);
            });
            
            await connection.StartAsync();

            await connection.SendAsync("SendMessageBy", "Kill", "Hello");
            // 保持监听状态
            while (true) ;

        }
    }
}