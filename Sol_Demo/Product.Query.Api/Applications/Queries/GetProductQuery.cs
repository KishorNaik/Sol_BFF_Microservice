using MediatR;

using SalesOrderProduct.Models.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Query.Api.Applications.Queries
{
    public class GetProductQuery : IRequest<IReadOnlyList<ProductModel>>
    {
        public String ProductIds { get; set; }
    }
}