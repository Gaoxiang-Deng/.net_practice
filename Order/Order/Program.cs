// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderProcessingSystem
{
    // 订单状态枚举
    public enum OrderStatus
    {
        Pending,       // 待处理
        Processing,    // 处理中
        Shipped,       // 已发货
        Delivered      // 已交付
    }

    // 订单类
    public class Order
    {
        public string OrderId { get; set; }
        public List<string> ProductList { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; private set; }

        public event Func<Order, Task> OnStatusChanged;

        public Order(string orderId, List<string> productList, decimal totalAmount)
        {
            OrderId = orderId;
            ProductList = productList;
            TotalAmount = totalAmount;
            Status = OrderStatus.Pending;
        }

        // 更新订单状态并触发事件通知
        public async Task UpdateStatusAsync(OrderStatus newStatus)
        {
            Status = newStatus;
            if (OnStatusChanged != null)
            {
                await OnStatusChanged(this);
            }
        }
    }

    // 订单处理服务
    public static class OrderService
    {
        public static async Task SendEmailNotificationAsync(Order order)
        {
            await Task.Delay(500); // 模拟异步发送邮件
            Console.WriteLine($"[Email] Order {order.OrderId} status changed to {order.Status}.");
        }

        public static async Task LogStatusChangeAsync(Order order)
        {
            await Task.Delay(300); // 模拟异步日志记录
            Console.WriteLine($"[Log] Order {order.OrderId} status updated to {order.Status}.");
        }

        public static async Task UpdateInventoryAsync(Order order)
        {
            await Task.Delay(400); // 模拟异步库存更新
            Console.WriteLine($"[Inventory] Inventory updated for order {order.OrderId}.");
        }
    }

    // 主程序入口
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // 创建订单
            var order = new Order("ORD12345", new List<string> { "Laptop", "Mouse" }, 1200.00m);

            // 订阅事件通知
            order.OnStatusChanged += OrderService.SendEmailNotificationAsync;
            order.OnStatusChanged += OrderService.LogStatusChangeAsync;
            order.OnStatusChanged += OrderService.UpdateInventoryAsync;

            // 显示订单创建信息
            Console.WriteLine($"Order {order.OrderId} created with total amount {order.TotalAmount:C}.");

            // 模拟状态变更
            await order.UpdateStatusAsync(OrderStatus.Processing);
            await order.UpdateStatusAsync(OrderStatus.Shipped);
            await order.UpdateStatusAsync(OrderStatus.Delivered);
        }
    }
}
