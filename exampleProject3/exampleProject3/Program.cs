using System;
using ECommerce.Models;

namespace ECommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 初始化库存管理和系统
            IInventoryManager inventoryManager = new InventoryManager();
            ECommerceSystem system = new ECommerceSystem(inventoryManager);

            // 创建商品
            IProduct laptop = new Electronic("Laptop", 1500);
            IProduct shirt = new Clothing("Shirt", 50);

            // 添加库存
            system.AddStock(laptop, 10);
            system.AddStock(shirt, 20);

            // 定义销售策略
            ISalesStrategy discount = new DiscountStrategy(10); // 10% 折扣

            // 执行销售
            system.SellProduct(laptop, 2, discount);
            system.SellProduct(shirt, 5, discount);
        }
    }

    // 电子商务系统
    public class ECommerceSystem
    {
        private readonly IInventoryManager _inventoryManager;

        public ECommerceSystem(IInventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
        }

        public void SellProduct(IProduct product, int quantity, ISalesStrategy salesStrategy)
        {
            if (_inventoryManager.ReduceStock(product, quantity))
            {
                salesStrategy.Apply(product);
                Console.WriteLine($"{quantity} units of {product.Name} sold at ${product.Price} each.");
            }
            else
            {
                Console.WriteLine($"Insufficient stock for {product.Name}.");
            }
        }

        public void AddStock(IProduct product, int quantity)
        {
            _inventoryManager.AddStock(product, quantity);
            Console.WriteLine($"{quantity} units of {product.Name} added to inventory.");
        }
    }
}