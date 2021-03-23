using MediatR;
using SalesOrderProduct.Models.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrderProduct.CompositeQuery.Api.Application.DomainQueries.Queries
{
    public class GetSalesOrderProductQuery : IRequest<List<SalesOrderHeaderModel>>
    {
        public ServerPagination Pagination { get; set; }
    }
}