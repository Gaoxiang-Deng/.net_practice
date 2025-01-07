using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderProcessingSystem1
{
    public enum OrderStatus
    {
        Pending,      
        Processing,    
        Shipped,      
        Delivered     
    }
    
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
        
        public async Task UpdateStatusAsync(OrderStatus newStatus)
        {
            Status = newStatus;
            if (OnStatusChanged != null)
            {
                await OnStatusChanged(this);
            }
        }
    }
    
    public static class OrderService
    {
        public static async Task SendEmailNotificationAsync(Order order)
        {
            await Task.Delay(200);
            Console.WriteLine($"[Email] OrderProcessingSystem1 {order.OrderId} status changed to {order.Status}.");
        }

        public static async Task LogStatusChangeAsync(Order order)
        {
            await Task.Delay(100);
            Console.WriteLine($"[Log] OrderProcessingSystem1 {order.OrderId} status updated to {order.Status}.");
        }
    }
}