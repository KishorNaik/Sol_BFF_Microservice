using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Product.Query.Api.Applications.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Query.Api.Configurations.Extensions
{
    public static class MassTransitConfigurationExtension
    {
        public static void AddMassTransitConfig(this IServiceCollection services, string rabbitMqUrl, string rabbitMqUserName, string rabbitMqPassword)
        {
            services.AddMassTransit((config) =>
            {
                config.AddConsumer<GetProductConsumer>();

                config.AddBus((busFactory) => Bus.Factory.CreateUsingRabbitMq((configRabbitMq) =>
                {
                    configRabbitMq.UseHealthCheck(busFactory);
                    configRabbitMq.Host(new Uri(rabbitMqUrl), (configHost) =>
                    {
                        configHost.Username(rabbitMqUserName);
                        configHost.Password(rabbitMqPassword);
                    });

                    configRabbitMq.ReceiveEndpoint("product-queue", (configReceiveEndPoint) =>
                    {
                        configReceiveEndPoint.ConfigureConsumer<GetProductConsumer>(busFactory);
                    });
                }));
            });

            services.AddMassTransitHostedService();
        }
    }
}