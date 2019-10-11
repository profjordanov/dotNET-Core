using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AsyncStreams
{
    class Order
    {
        public int Id { get; set; }
    }

    class OrderFactory
    {
        public async IAsyncEnumerable<Order> MakeOrdersAsync(int count)
        {
            for (var i = 0; i < count; i++)
            {
                // Pretend to call some expensive process to build up the order.
                await Task.Delay(1000);

                yield return new Order { Id = i + 1 };
            }
        }
    }
}
