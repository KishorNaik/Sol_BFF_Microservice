using AutoMapper;
using MassTransit;
using MediatR;
using SalesOrder.Query.Api.Applications.DomainQueries.Queries;

using SalesOrderProduct.Models.Shared.Models;
using SalesOrderProduct.Models.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.Query.Api.Applications.Messages
{
    public class GetSalesOrderConsumer : IConsumer<SalesOrderHeaderModel>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public GetSalesOrderConsumer(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        async Task IConsumer<SalesOrderHeaderModel>.Consume(ConsumeContext<SalesOrderHeaderModel> context)
        {
            var resultSet = (await mediator?.Send<IReadOnlyList<SalesOrderHeaderModel>>(new GetSalesOrderQuery()
            {
                PageNumber = context.Message.Pagination.PageNumber,
                Rows = context.Message.Pagination.RowsOfPage
            })
            )?.ToList();

            var salesOrderResponse = new SalesOrderResponse()
            {
                SalesOrderHeaderModels = resultSet
            };

            await context.RespondAsync<SalesOrderResponse>(salesOrderResponse);
        }
    }
}