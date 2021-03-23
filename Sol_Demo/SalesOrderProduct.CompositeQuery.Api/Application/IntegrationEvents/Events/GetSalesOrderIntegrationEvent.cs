using MediatR;
using SalesOrderProduct.Models.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrderProduct.CompositeQuery.Api.Application.IntegrationEvents.Events
{
    public class GetSalesOrderIntegrationEvent : IRequest<List<SalesOrderHeaderModel>>
    {
        #region Non Domain Property

        public int PageNumber { get; set; }

        public int Rows { get; set; }

        #endregion Non Domain Property
    }
}