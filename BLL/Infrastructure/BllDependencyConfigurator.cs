using BLL.Interfaces;
using BLL.Services;
using DAL.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Infrastructure
{
    public static class BllDependencyConfigurator
    {
        public static void ConfigureServices(IServiceCollection serviceCollection, string connectionString)
        {
            DalDependencyConfigurator.ConfigureServices(serviceCollection, connectionString);

            serviceCollection.AddTransient<ICustomerService, CustomerService>();
            serviceCollection.AddTransient<IOrderService, OrderService>();
            serviceCollection.AddTransient<IProductService, ProductService>();
            serviceCollection.AddTransient<IStatisticService, StatisticService>();
            serviceCollection.AddAutoMapper(configExpression =>
            {
                configExpression.AddProfile(new WebMarketMapperProfile());
            });
        }
    }
}
