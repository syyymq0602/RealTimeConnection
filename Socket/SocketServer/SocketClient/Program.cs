using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketClient
{
    internal class Program
    {
        private static byte[] _store = new byte[1024];
        private static Socket clientSocket;
        public static void Main(string[] args)
        {
            // 设定服务器IP地址
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(ipAddress, 8855));
                Console.WriteLine("连接成功!");
            }
            catch(SocketException ex)
            {
                Console.WriteLine("连接服务器失败，请按回车键退出！");
                Console.WriteLine(ex.ToString());
                return;
            }
            //通过clientSocket接收数据 
            int receiveLength = clientSocket.Receive(_store); 
            Console.WriteLine("接收服务器消息：{0}",Encoding.ASCII.GetString(_store,0,receiveLength));
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "exit")
                    break;
                _store = Encoding.ASCII.GetBytes(input);
                clientSocket.Send(_store);
            }
            Console.WriteLine("disconnect form server");
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
    }
}