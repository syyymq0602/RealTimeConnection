using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var re = SayHello(client);
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        public static async Task SayHello(Greeter.GreeterClient client)
        {
            var reply = await client.SayHelloAsync(new HelloRequest
            {
                Name = "GrpcClient  ",
            });
            Console.WriteLine("Greeting: " + reply.Message);
        }
    }
}
