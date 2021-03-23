using MediatR;
using SalesOrderProduct.Models.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.Query.Api.Applications.DomainQueries.Queries
{
    public class GetSalesOrderQuery : IRequest<IReadOnlyList<SalesOrderHeaderModel>>
    {
        #region Non Domain Property

        public int PageNumber { get; set; }

        public int Rows { get; set; }

        #endregion Non Domain Property
    }
}