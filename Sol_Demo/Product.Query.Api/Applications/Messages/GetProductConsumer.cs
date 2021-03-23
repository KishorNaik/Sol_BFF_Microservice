using AutoMapper;
using MassTransit;
using MediatR;
using Product.Query.Api.Applications.Queries;
using SalesOrderProduct.Models.Shared.Models;
using SalesOrderProduct.Models.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Query.Api.Applications.Messages
{
    public class GetProductConsumer : IConsumer<ProductModel>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public GetProductConsumer(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        async Task IConsumer<ProductModel>.Consume(ConsumeContext<ProductModel> context)
        {
            var result = (await mediator.Send<IReadOnlyList<ProductModel>>(new GetProductQuery()
            {
                ProductIds = context.Message.ProductIds
            })
            )?.ToList();

            var productResponse = new ProductResponse()
            {
                Products = result
            };

            await context.RespondAsync<ProductResponse>(productResponse);
        }
    }
}