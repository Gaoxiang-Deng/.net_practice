// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

// 商品接口
public interface IProduct
{
    string Name { get; }
    decimal Price { get; }
    void ApplyDiscount(decimal percentage);
}

// 具体商品实现
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

// 主程序
public class Program
{
    public static void Main(string[] args)
    {
        IInventoryManager inventoryManager = new InventoryManager();
        ECommerceSystem system = new ECommerceSystem(inventoryManager);

        IProduct laptop = new Electronic("Laptop", 1500);
        IProduct shirt = new Clothing("Shirt", 50);

        system.AddStock(laptop, 10);
        system.AddStock(shirt, 20);

        ISalesStrategy discount = new DiscountStrategy(10); // 10% 折扣

        system.SellProduct(laptop, 2, discount);
        system.SellProduct(shirt, 5, discount);
    }
}
