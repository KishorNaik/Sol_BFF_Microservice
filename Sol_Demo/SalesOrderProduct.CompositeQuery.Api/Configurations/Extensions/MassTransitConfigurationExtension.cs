using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrderProduct.CompositeQuery.Api.Configurations.Extensions
{
    public static class MassTransitConfigurationExtension
    {
        public static void AddMassTransitConfig(this IServiceCollection services, string rabbitMQUrl, String rabbitMqUserName, string rabbitMqPassword)
        {
            services.AddMassTransit((config) =>
            {
                config.AddBus((busFactory) => Bus.Factory.CreateUsingRabbitMq((configRabbitMq) =>
                {
                    configRabbitMq.UseHealthCheck(busFactory);
                    configRabbitMq.Host(new Uri(rabbitMQUrl), (configHost) =>
                    {
                        configHost.Username(rabbitMqUserName);
                        configHost.Password(rabbitMqPassword);
                    }
                    );
                }));
            });
            services.AddMassTransitHostedService();
        }
    }
}