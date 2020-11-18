using System;
using System.Threading.Tasks;
using ChatServer.Dto;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace ChatServer.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Datas datas)
        {
            string jsonData = JsonConvert.SerializeObject(datas);
            await Clients.Others.SendAsync("ReceiveMessage", jsonData);
            Console.WriteLine(jsonData);
        }
        public async Task SendMessageBy(string user,string message)
        {
            await Clients.All.SendAsync("ReceiveMessageBy", user,message);
            Console.WriteLine(user + "    " + message);
        }
        public async Task SendMessageTime(double time)
        {
            await Clients.All.SendAsync("ReceiveTime", time);
            Console.WriteLine(time);
        }
        
    }
}