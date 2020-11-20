using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcGreeter
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var reply = new HelloReply
            {
                Message = "Hello " + request.Name
            };
            var str = request.Name;
            Console.WriteLine(str);
            return Task.FromResult(reply);
        }
        public override async Task StreamingFromServer(ExampleRequest request, IServerStreamWriter<ExampleResponse> responseStream, ServerCallContext context)
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await responseStream.WriteAsync(new ExampleResponse());
                await Task.Delay(TimeSpan.FromSeconds(1), context.CancellationToken);
            }
        }
        public override async Task<ExampleResponse> StreamingFromClient(IAsyncStreamReader<ExampleRequest> requestStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                var message = requestStream.Current;
                // ...
            }
            return new ExampleResponse();
        }
        public override async Task StreamingBothWays(IAsyncStreamReader<ExampleRequest> requestStream,IServerStreamWriter<ExampleResponse> responseStream, ServerCallContext context)
        {
            // Read requests in a background task.
            var readTask = Task.Run(async () =>
            {
                await foreach (var message in requestStream.ReadAllAsync())
                {
                    // Process request.
                }
            });

            // Send responses until the client signals that it is complete.
            while (!readTask.IsCompleted)
            {
                await responseStream.WriteAsync(new ExampleResponse());
                await Task.Delay(TimeSpan.FromSeconds(1), context.CancellationToken);
            }
        }
    }
}