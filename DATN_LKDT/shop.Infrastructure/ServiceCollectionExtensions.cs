using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using shop.Infrastructure.Business;
using shop.Infrastructure.Business.VirtualItem;
using shop.Infrastructure.Database.Context;
using shop.Infrastructure.Intercepter;
using shop.Infrastructure.Repositories.VirtualItem;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServiceMngComponents(this IServiceCollection services,
       IConfiguration configuration)
        {
            

            //Virtual Item 
            services.AddScoped<IVirtualItemRepository, VirtualItemRepository>();
            services.AddScoped<IVirtualItemBusiness, VirtualItemBusiness>();
            return services;
        }
    }
}
