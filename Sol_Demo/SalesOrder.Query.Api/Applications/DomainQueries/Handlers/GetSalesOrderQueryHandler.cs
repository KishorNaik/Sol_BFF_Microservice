using AutoMapper;
using MediatR;
using SalesOrder.Query.Api.Applications.DomainQueries.Queries;
using SalesOrder.Query.Api.Infrastructures.RepositoryQueries;
using SalesOrderProduct.Models.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalesOrder.Query.Api.Applications.DomainQueries.Handlers
{
    public class GetSalesOrderQueryHandler : IRequestHandler<GetSalesOrderQuery, IReadOnlyList<SalesOrderHeaderModel>>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public GetSalesOrderQueryHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        Task<IReadOnlyList<SalesOrderHeaderModel>> IRequestHandler<GetSalesOrderQuery, IReadOnlyList<SalesOrderHeaderModel>>.Handle(GetSalesOrderQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getSalesOrderData = mediator?.Send<IReadOnlyList<SalesOrderHeaderModel>>(mapper.Map<GetSalesOrderRepositoryQuery>(request));
                return getSalesOrderData;
            }
            catch
            {
                throw;
            }
        }
    }
}