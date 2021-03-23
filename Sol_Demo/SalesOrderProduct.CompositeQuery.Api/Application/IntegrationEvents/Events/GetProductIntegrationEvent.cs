using MediatR;
using SalesOrderProduct.Models.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrderProduct.CompositeQuery.Api.Application.IntegrationEvents.Events
{
    public class GetProductIntegrationEvent : IRequest<List<ProductModel>>
    {
        public String ProductIds { get; set; }
    }
}