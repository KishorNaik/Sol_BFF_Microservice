using MediatR;
using SalesOrderProduct.Models.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.Query.Api.Infrastructures.RepositoryQueries
{
    public class GetSalesOrderRepositoryQuery : SalesOrderHeaderModel, IRequest<IReadOnlyList<SalesOrderHeaderModel>>
    {
    }
}