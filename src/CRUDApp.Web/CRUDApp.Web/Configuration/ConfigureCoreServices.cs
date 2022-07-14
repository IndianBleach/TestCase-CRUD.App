using CRUDApp.Core.Interfaces;
using CRUDApp.Infrastructure.Services.OrderItemService;
using CRUDApp.Infrastructure.Services.OrderService;

namespace CRUDApp.Web.Configuration
{
    public static class ConfigureCoreServices
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IOrderService, OrderService>();

            services.AddTransient<IOrderItemService, OrderItemService>();
        }
    }
}
