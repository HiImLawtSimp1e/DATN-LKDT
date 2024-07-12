using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using shop.Infrastructure.Database.Context;
using shop.Infrastructure;

namespace Shop.WebAdmin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


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
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}