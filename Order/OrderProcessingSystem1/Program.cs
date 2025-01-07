using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderProcessingSystem1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var order = new Order("ORD12345", new List<string> { "Laptop", "Mouse" }, 1200.00m);
            
            order.OnStatusChanged += OrderService.SendEmailNotificationAsync;
            order.OnStatusChanged += OrderService.LogStatusChangeAsync;
            
            Console.WriteLine($"OrderProcessingSystem1 {order.OrderId} created with total amount {order.TotalAmount:C}.");
            
            await order.UpdateStatusAsync(OrderStatus.Processing);
            await order.UpdateStatusAsync(OrderStatus.Shipped);
            await order.UpdateStatusAsync(OrderStatus.Delivered);
        }
    }
}