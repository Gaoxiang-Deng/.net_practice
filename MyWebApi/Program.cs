using Microsoft.OpenApi.Models;
using MyWebApi.Controllers;
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
        Description = "A simple API to manage students"
    });
});

// Add temporary CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Add StudentService
builder.Services.AddSingleton<StudentService>();

// Configure Kestrel server to use port 5001
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5001); // Bind to port 5001
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Students API v1");
        options.RoutePrefix = string.Empty; // Set Swagger UI at the root
    });
}

app.UseCors();
app.UseHttpsRedirection();

// Map controllers
app.MapStudentEndpoints();

app.Run();