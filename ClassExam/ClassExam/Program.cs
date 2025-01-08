// See https://aka.ms/new-console-template for more information

using System;

public static class Logger
{
    public static void Log<T>(T message, string logLevel = "INFO")
    {
        // 格式化日志内容，添加时间戳和日志级别
        string formattedLog = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{logLevel}] {message}";
        
        // 输出到控制台
        Console.WriteLine(formattedLog);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // 示例用法
        Logger.Log("程序启动");
        Logger.Log(123);
        Logger.Log(new { Name = "Alice", Age = 25 });
    }
}
