using System;
using System.Threading;

namespace AsyncStreams
{
    class Program
    {
        static int ThreadId => Thread.CurrentThread.ManagedThreadId;

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var orderFactory = new OrderFactory();

            Console.WriteLine($"[{ThreadId}]Enumerating orders...");

            await foreach (var order in orderFactory.MakeOrdersAsync(5))
            {
                Console.WriteLine($"[{ThreadId}]Received order {order.Id}.");
            }

            Console.WriteLine($"[{ThreadId}]All orders created!");
        }
    }
}
