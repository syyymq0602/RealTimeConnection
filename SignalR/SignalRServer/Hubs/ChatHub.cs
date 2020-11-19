using System;
using System.Threading.Tasks;
using ChatServer.Dto;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace ChatServer.Hubs
{
    // 强类型
    public class ChatHub : Hub<IChatClient>
    {
        public async Task SendMessage(Datas datas)
        {
            datas.num += 10;
            string jsonData = JsonConvert.SerializeObject(datas);
            await Clients.All.ReceiveMessage(jsonData);
            Console.WriteLine(Context.ConnectionId);
        }
        public async Task SendMessageBy(string user,string message)
        {
            Console.WriteLine(user + "    " + message);
            user = "Hongko";
            message = "JoJo";
            await Clients.All.ReceiveMessageBy(user,message);
            Console.WriteLine(Context.ConnectionId);
        }
        public async Task SendMessageTime(Datas data)
        {
            await Clients.User(Context.ConnectionId).ReceiveData(data);
        }
        // 将用户加入组
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,groupName);
            await Clients.Group(groupName).ReceiveMessageBy("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }
        // 将用户移除组
        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).ReceiveMessageBy("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }

    }
}