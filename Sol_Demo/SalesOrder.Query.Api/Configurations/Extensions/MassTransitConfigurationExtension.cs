using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using SalesOrder.Query.Api.Applications.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.Query.Api.Configurations.Extensions
{
    public static class MassTransitConfigurationExtension
    {
        public static void AddMassTransitConfig(this IServiceCollection services, string rabbitMqUrl, string rabbitMqUserName, string rabbitMqPassword)
        {
            services.AddMassTransit((config) =>
            {
                config.AddConsumer<GetSalesOrderConsumer>();

                config.AddBus((busFactory) => Bus.Factory.CreateUsingRabbitMq((configRabbitMq) =>
                {
                    configRabbitMq.UseHealthCheck(busFactory);
                    configRabbitMq.Host(new Uri(rabbitMqUrl), (configHost) =>
                    {
                        configHost.Username(rabbitMqUserName);
                        configHost.Password(rabbitMqPassword);
                    });

                    configRabbitMq.ReceiveEndpoint("sales-order-queue", (configReceiveEndPoint) =>
                    {
                        configReceiveEndPoint.ConfigureConsumer<GetSalesOrderConsumer>(busFactory);
                    });
                }));
            });

            services.AddMassTransitHostedService();
        }
    }
}