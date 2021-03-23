using AutoMapper;
using MediatR;
using Product.Query.Api.Applications.Queries;
using Product.Query.Api.Infrastructures.RepositoryQueries;
using SalesOrderProduct.Models.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Query.Api.Applications.Handlers
{
    public sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, IReadOnlyList<ProductModel>>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public GetProductQueryHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        Task<IReadOnlyList<ProductModel>> IRequestHandler<GetProductQuery, IReadOnlyList<ProductModel>>.Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var resultTask = mediator.Send<IReadOnlyList<ProductModel>>(mapper.Map<GetProductRepositoryQuery>(request));
                return resultTask;
            }
            catch
            {
                throw;
            }
        }
    }
}