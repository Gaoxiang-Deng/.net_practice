using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderProcessingSystem.Models
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

    // 订单服务
    public static class OrderService
    {
        public static async Task SendEmailNotificationAsync(Order order)
        {
            await Task.Delay(200); // 模拟异步发送邮件
            Console.WriteLine($"[Email] Order {order.OrderId} status changed to {order.Status}.");
        }

        public static async Task LogStatusChangeAsync(Order order)
        {
            await Task.Delay(100); // 模拟异步日志记录
            Console.WriteLine($"[Log] Order {order.OrderId} status updated to {order.Status}.");
        }
    }
}