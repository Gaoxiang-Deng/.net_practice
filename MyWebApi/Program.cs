using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyWebApi.Controllers;
using MyWebApi.Data;
using MyWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Students API",
        Version = "v1",
        Description = "A complete API to manage students with database integration"
    });
});

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Configure database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        "Server=localhost;Database=studentsdb;User=root;Password=hyP3x!sd;",
        new MySqlServerVersion(new Version(9, 1, 0)) // 替换为实际 MySQL 版本
    ));

// Add StudentService
builder.Services.AddScoped<StudentService>();

var app = builder.Build();

// Apply migrations and seed data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    ApplicationDbContext.SeedData(dbContext);
}

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Students API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors();
app.UseHttpsRedirection();

// Map endpoints from controllers
app.MapStudentEndpoints();

app.Run();