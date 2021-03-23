using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesOrder.Query.Api.Applications.DomainQueries.Queries;
using SalesOrderProduct.Models.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.Query.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/sales-order")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {
        // Testing

        //private readonly IMediator mediator = null;

        //public SalesOrderController(IMediator mediator)
        //{
        //    this.mediator = mediator;
        //}

        //[HttpPost("get-sales-order-data")]
        //public async Task<IActionResult> TestGetSalesOrderData([FromBody] GetSalesOrderQuery getSalesOrderQuery)
        //{
        //    var result = await mediator.Send<IReadOnlyList<SalesOrderHeaderModel>>(getSalesOrderQuery);

        //    return base.Ok(result);
        //}
    }
}