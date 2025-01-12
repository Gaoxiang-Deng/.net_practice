using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyWebApi.Data;
using MyWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure sensitive information via appsettings.json or environment variables
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       "Server=localhost;Database=studentsdb;User=root;Password=yourpassword;";

// Add services to the container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 32)),
        mysqlOptions => mysqlOptions.EnableStringComparisonTranslations()
    ));
builder.Services.AddScoped<StudentService>();
builder.Services.AddControllers();
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
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Apply database migrations and seed data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    ApplicationDbContext.SeedData(dbContext);
}

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
app.UseAuthorization();
app.MapControllers();
app.Run();