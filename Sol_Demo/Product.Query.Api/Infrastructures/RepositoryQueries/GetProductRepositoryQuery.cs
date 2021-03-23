using MediatR;
using SalesOrderProduct.Models.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Query.Api.Infrastructures.RepositoryQueries
{
    public class GetProductRepositoryQuery : ProductModel, IRequest<IReadOnlyList<ProductModel>>
    {
    }
}