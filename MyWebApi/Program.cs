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
        Description = "A complete API to manage students with filtering, sorting, and pagination"
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

// Add StudentService
builder.Services.AddSingleton<StudentService>();

var app = builder.Build();

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