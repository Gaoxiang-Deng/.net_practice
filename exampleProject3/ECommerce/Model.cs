// Models.cs
using System;
using System.Collections.Generic;

namespace ECommerce.Models
{
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

    public interface IProduct
    {
        string Name { get; }
        decimal Price { get; }
        void ApplyDiscount(decimal percentage);
    }

    public class Electronic : IProduct
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Electronic(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public void ApplyDiscount(decimal percentage)
        {
            Price -= Price * percentage / 100;
        }
    }

    public class Clothing : IProduct
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Clothing(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public void ApplyDiscount(decimal percentage)
        {
            Price -= Price * percentage / 100;
        }
    }

    public interface IInventoryManager
    {
        void AddStock(IProduct product, int quantity);
        bool ReduceStock(IProduct product, int quantity);
        int GetStock(IProduct product);
    }

    public class InventoryManager : IInventoryManager
    {
        private readonly Dictionary<IProduct, int> _inventory = new();

        public void AddStock(IProduct product, int quantity)
        {
            if (_inventory.ContainsKey(product))
                _inventory[product] += quantity;
            else
                _inventory[product] = quantity;
        }

        public bool ReduceStock(IProduct product, int quantity)
        {
            if (_inventory.ContainsKey(product) && _inventory[product] >= quantity)
            {
                _inventory[product] -= quantity;
                return true;
            }
            return false;
        }

        public int GetStock(IProduct product)
        {
            return _inventory.ContainsKey(product) ? _inventory[product] : 0;
        }
    }

    public interface ISalesStrategy
    {
        void Apply(IProduct product);
    }

    public class DiscountStrategy : ISalesStrategy
    {
        private readonly decimal _discountPercentage;

        public DiscountStrategy(decimal discountPercentage)
        {
            _discountPercentage = discountPercentage;
        }

        public void Apply(IProduct product)
        {
            product.ApplyDiscount(_discountPercentage);
        }
    }
}
