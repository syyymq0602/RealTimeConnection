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
            await connection.StartAsync();
            connection.On<string>("ReceiveMessage", data =>
            {
                Console.WriteLine(data);
                var user = JsonConvert.DeserializeObject<Datas>(data);
                Console.WriteLine(user.num +"  "+user.size);
            });
            

            await connection.SendAsync("SendMessageBy", "Kill", "Hello");
        }
    }
}