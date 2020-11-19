using ChatServer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string str);
        Task ReceiveMessageBy(string user,string message);
        Task ReceiveData(Datas data);
    }
}
