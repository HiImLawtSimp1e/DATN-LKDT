using MicroBase.Entity.Repositories;
using Microsoft.EntityFrameworkCore;
using shop.Application.Interfaces;
using shop.Application.Services;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;

var builder = WebApplication.CreateBuilder(args);

// Register the DbContext with a connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
builder.Services.AddScoped<IApplicationRoleService, ApplicationRoleService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Register the repository
builder.Services.AddScoped<IRepository<BlogEntity>, Repository<BlogEntity>>();
builder.Services.AddScoped<IRepository<AccountEntity>, Repository<AccountEntity>>();
builder.Services.AddScoped<IRepository<ApplicationUser>, Repository<ApplicationUser>>();
builder.Services.AddScoped<IRepository<ApplicationRole>, Repository<ApplicationRole>>();

//Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000", "https://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
