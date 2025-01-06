using System.Collections.Generic;

namespace ECommerce.Models
{
    // 商品接口
    public interface IProduct
    {
        string Name { get; }
        decimal Price { get; }
        void ApplyDiscount(decimal percentage);
    }

    // 商品类：电子产品
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

    // 商品类：服装
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

    // 库存管理接口
    public interface IInventoryManager
    {
        void AddStock(IProduct product, int quantity);
        bool ReduceStock(IProduct product, int quantity);
        int GetStock(IProduct product);
    }

    // 库存管理实现
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

    // 销售策略接口
    public interface ISalesStrategy
    {
        void Apply(IProduct product);
    }

    // 折扣策略实现
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
