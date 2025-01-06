using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderProcessingSystem.Models;

namespace OrderProcessingSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // 创建订单
            var order = new Order("ORD12345", new List<string> { "Laptop", "Mouse" }, 1200.00m);

            // 订阅状态变更事件
            order.OnStatusChanged += OrderService.SendEmailNotificationAsync;
            order.OnStatusChanged += OrderService.LogStatusChangeAsync;

            // 显示订单创建信息
            Console.WriteLine($"Order {order.OrderId} created with total amount {order.TotalAmount:C}.");

            // 更新订单状态
            await order.UpdateStatusAsync(OrderStatus.Processing);
            await order.UpdateStatusAsync(OrderStatus.Shipped);
            await order.UpdateStatusAsync(OrderStatus.Delivered);
        }
    }
}