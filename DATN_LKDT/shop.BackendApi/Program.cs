using Microsoft.EntityFrameworkCore;
using shop.Infrastructure;
using shop.Infrastructure.Database.Context;

var builder = WebApplication.CreateBuilder(args);


// Đọc cấu hình từ appsettings.json
var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
var configuration = configurationBuilder.Build();

// Thêm cấu hình vào WebApplicationBuilder
builder.Configuration.AddConfiguration(configuration);

// Thêm DbContext vào WebApplicationBuilder
var connectionString = builder.Configuration.GetSection("ConnectionStrings:Default").Value;
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.RegisterServiceMngComponents(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
