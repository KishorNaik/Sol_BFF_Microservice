using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesOrderProduct.CompositeQuery.Api.Application.DomainQueries.Queries;
using SalesOrderProduct.Models.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrderProduct.CompositeQuery.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/sales-order-product")]
    [ApiController]
    public class SalesOrderProductController : ControllerBase
    {
        private readonly IMediator mediator = null;

        public SalesOrderProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("data")]
        public async Task<IActionResult> GetSalesOrderProductData([FromBody] GetSalesOrderProductQuery getSalesOrderProductQuery)
        {
            var results = await mediator.Send<List<SalesOrderHeaderModel>>(getSalesOrderProductQuery);
            return base.Ok(results);
        }
    }
}