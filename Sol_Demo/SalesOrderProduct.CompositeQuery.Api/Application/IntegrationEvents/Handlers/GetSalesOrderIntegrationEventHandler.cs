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
    public class GetSalesOrderIntegrationEventHandler : IRequestHandler<GetSalesOrderIntegrationEvent, List<SalesOrderHeaderModel>>
    {
        private readonly IBus bus = null;
        private readonly IMapper mapper = null;

        public GetSalesOrderIntegrationEventHandler(IBus bus, IMapper mapper)
        {
            this.bus = bus;
            this.mapper = mapper;
        }

        async Task<List<SalesOrderHeaderModel>> IRequestHandler<GetSalesOrderIntegrationEvent, List<SalesOrderHeaderModel>>.Handle(GetSalesOrderIntegrationEvent request, CancellationToken cancellationToken)
        {
            var client = bus.CreateRequestClient<SalesOrderHeaderModel>(new Uri("queue:sales-order-queue"));

            var response = await client.GetResponse<SalesOrderResponse>(mapper.Map<SalesOrderHeaderModel>(request));

            //var response = await client.GetResponse<SalesOrderResponse>(new SalesOrderHeaderModel()
            //{
            //    Pagination = new ServerPagination()
            //    {
            //        PageNumber = request.PageNumber,
            //        RowsOfPage = request.Rows
            //    }
            //});

            return response.Message.SalesOrderHeaderModels;
        }
    }
}