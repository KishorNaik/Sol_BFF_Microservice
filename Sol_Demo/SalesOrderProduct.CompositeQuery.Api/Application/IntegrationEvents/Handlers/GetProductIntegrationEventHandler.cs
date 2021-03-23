using AutoMapper;
using MassTransit;
using MediatR;
using SalesOrderProduct.CompositeQuery.Api.Application.IntegrationEvents.Events;
using SalesOrderProduct.Models.Shared.Models;
using SalesOrderProduct.Models.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalesOrderProduct.CompositeQuery.Api.Application.IntegrationEvents.Handlers
{
    public class GetProductIntegrationEventHandler : IRequestHandler<GetProductIntegrationEvent, List<ProductModel>>
    {
        private readonly IBus bus = null;
        private readonly IMapper mapper = null;

        public GetProductIntegrationEventHandler(IBus bus, IMapper mapper)
        {
            this.mapper = mapper;
            this.bus = bus;
        }

        async Task<List<ProductModel>> IRequestHandler<GetProductIntegrationEvent, List<ProductModel>>.Handle(GetProductIntegrationEvent request, CancellationToken cancellationToken)
        {
            var client = bus.CreateRequestClient<ProductModel>(new Uri("queue:product-queue"));

            var response = await client.GetResponse<ProductResponse>(mapper.Map<ProductModel>(request));

            return response.Message.Products;
        }
    }
}