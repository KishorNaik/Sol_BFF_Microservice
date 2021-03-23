using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Query.Api.Applications.Queries;
using SalesOrderProduct.Models.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Query.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // Testing

        //private readonly IMediator mediator = null;

        //public ProductController(IMediator mediator)
        //{
        //    this.mediator = mediator;
        //}

        //[HttpPost("get-products")]
        //public async Task<IActionResult> GetProducts([FromBody] GetProductQuery getProductQuery)
        //{
        //    var result = (await mediator.Send<IReadOnlyList<ProductModel>>(getProductQuery))?.ToList();
        //    return base.Ok(result);
        //}
    }
}