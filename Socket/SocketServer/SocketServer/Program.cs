using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketServer
{
    internal class Program
    {
        private static byte[] _result = new byte[1024];
        private static Socket _serverSocket;
        public static void Main(string[] args)
        {
            // 服务器地址
            IPAddress IP =IPAddress.Parse("127.0.0.1");
            _serverSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            _serverSocket.Bind(new IPEndPoint(IP,8855));
            _serverSocket.Listen(10);
            Console.WriteLine("启动监听{0}成功", _serverSocket.LocalEndPoint);
            // 使用线程向Client发送数据
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
            Console.ReadLine();
        }
        /// <summary> 
        /// 监听客户端连接 
        /// </summary> 
        private static void ListenClientConnect() 
        { 
            while (true) 
            { 
                Socket clientSocket = _serverSocket.Accept(); 
                clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello")); 
                Thread receiveThread = new Thread(ReceiveMessage); 
                receiveThread.Start(clientSocket); 
            } 
        } 
   
        /// <summary> 
        /// 接收消息 
        /// </summary> 
        /// <param name="clientSocket"></param> 
        private static void ReceiveMessage(object clientSocket) 
        { 
            Socket myClientSocket = (Socket)clientSocket; 
            while (true) 
            { 
                try 
                { 
                    //通过clientSocket接收数据 
                    int receiveNumber = myClientSocket.Receive(_result);
                    var message = Encoding.ASCII.GetString(_result, 0, receiveNumber);
                    Console.WriteLine("接收客户端{0}消息{1}", myClientSocket.RemoteEndPoint, message); 
                } 
                catch(Exception ex) 
                { 
                    Console.WriteLine(ex.Message); 
                    myClientSocket.Shutdown(SocketShutdown.Both); 
                    myClientSocket.Close(); 
                    break; 
                } 
            } 
        } 
    }
}