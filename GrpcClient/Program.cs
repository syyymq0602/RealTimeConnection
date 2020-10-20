using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer.Web.Protos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
          
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new EmployeeService.EmployeeServiceClient(channel);

            var option = int.Parse(args[0]);
            switch (option)
            {
                case 1:
                    await GetByNoAsync(client);
                    break;
                case 2:
                    await GetAllAsync(client);
                    break;
                case 3:
                    await AddPhotoAsync(client);
                    break;
                case 5:
                    await SaveAllAsync(client);
                    break;
            }

            
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        public static async Task GetByNoAsync(EmployeeService.EmployeeServiceClient client)
        {
            var md = new Metadata
            {
                {"username","dave" },
                {"role","administrator" }
            };

            var response = await client.GetByNoAsync(new GetByNoRequest
            {
                No = 1994
            }, md);

            Console.WriteLine($"Response message:{response}");
        }

        public static async Task GetAllAsync(EmployeeService.EmployeeServiceClient client)
        {
            using var call = client.GetAll(new GetAllRequest());
            var responseStream = call.ResponseStream;
            while(await responseStream.MoveNext())
            {
                Console.WriteLine(responseStream.Current.Employee);
            }
        }
        public static async Task AddPhotoAsync(EmployeeService.EmployeeServiceClient client)
        {
            var md = new Metadata
            {
                {"username","dave" },
                {"role","administrator" }
            };
            FileStream fs = File.OpenRead("36.png");
            using var call = client.AddPhoto(md);

            var stream = call.RequestStream;
            while (true)
            {
                byte[] buffer = new byte[1024];
                int numRead = await fs.ReadAsync(buffer, 0, buffer.Length);
                if(numRead == 0)
                {
                    break;
                }

                if(numRead < buffer.Length)
                {
                    Array.Resize(ref buffer , numRead);
                }

                await stream.WriteAsync(new AddPhotoRequest()
                {
                    Data = ByteString.CopyFrom(buffer)
                }) ;
            }
            // 传输完成返回响应
            await stream.CompleteAsync();

            var res = await call.ResponseAsync;

            Console.WriteLine(res.IsOk);
        }

        public static async Task SaveAllAsync(EmployeeService.EmployeeServiceClient client)
        {
            var employees = new List<Employee>
           {
               new Employee
               {
                   No=111,
                   FirstName="EEEEE",
                   LastName="Geller",
                   Salary=7800.9f
               },
               new Employee
               {
                   No=222,
                   FirstName="John",
                   LastName="Tril",
                   Salary=533f
               }
           };

            using var call = client.SaveAll();
            var requestStream = call.RequestStream;
            var responseStream = call.ResponseStream;

            var responseTask = Task.Run(async () =>
            {
                while (await responseStream.MoveNext())
                {
                    Console.WriteLine($"Saved:{ responseStream.Current.Employee }");
                }
            });

            // 发送请求的逻辑
            foreach (var employee in employees)
            {
                await requestStream.WriteAsync(new EmployeeRequest
                {
                    Employee = employee
                });
            }

            await requestStream.CompleteAsync();
            await responseTask;        }
    }
}
