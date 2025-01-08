// Program.cs
using System;
using Microsoft.Extensions.DependencyInjection;
using ECommerce.Models;

namespace ECommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            
            serviceCollection.AddSingleton<IInventoryManager, InventoryManager>();
            serviceCollection.AddSingleton<ISalesStrategy>(provider => new DiscountStrategy(10)); // 10% 折扣
            serviceCollection.AddTransient<ECommerceSystem>();


            var serviceProvider = serviceCollection.BuildServiceProvider();


            var system = serviceProvider.GetRequiredService<ECommerceSystem>();


            IProduct laptop = new Electronic("Laptop", 1500);
            IProduct shirt = new Clothing("Shirt", 50);
            
            system.AddStock(laptop, 10);
            system.AddStock(shirt, 20);

           
            var discountStrategy = serviceProvider.GetRequiredService<ISalesStrategy>();

 
            system.SellProduct(laptop, 2, discountStrategy);
            system.SellProduct(shirt, 5, discountStrategy);
        }
    }
}