using AutoMapper;
using MediatR;
using SalesOrderProduct.CompositeQuery.Api.Application.DomainQueries.Queries;
using SalesOrderProduct.CompositeQuery.Api.Application.IntegrationEvents.Events;
using SalesOrderProduct.Models.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace SalesOrderProduct.CompositeQuery.Api.Application.DomainQueries.Handlers
{
    public sealed class GetSalesOrderProductQueryHandler : IRequestHandler<GetSalesOrderProductQuery, List<SalesOrderHeaderModel>>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public GetSalesOrderProductQueryHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        private Task<String> GetProductIdsAsync(List<SalesOrderHeaderModel> salesOrderHeaderModels)
        {
            return Task.Run(() =>
            {
                var productId =
                salesOrderHeaderModels
                .AsParallel()
                .AsSequential()
                .Select((salesOrderModelSelect) =>
                {
                    var productModelIdList =
                     salesOrderModelSelect
                         .SalesOrderDetails
                         .AsParallel()
                         .AsSequential()
                         .Select((salesOrderDetailsModel) => new ProductModel()
                         {
                             ProductId = salesOrderDetailsModel.ProductID
                         })
                         .ToList();

                    return productModelIdList;
                })
                .ToList()
                .SelectMany((x) => x)
                .Select((x) => x.ProductId)
                .ToArray();

                return String.Join(",", productId);
            });
        }

        private Task<List<SalesOrderHeaderModel>> SalesOrderProductJoin(List<SalesOrderHeaderModel> salesOrderHeaderModels, List<ProductModel> productModels)
        {
            salesOrderHeaderModels
                .AsParallel()
                .AsSequential()
               .Select((salesOrderHeaderModel) =>
               {
                   var salesOrderDetailsProductData =
                    salesOrderHeaderModel
                     .SalesOrderDetails
                     .AsParallel()
                     .AsSequential()
                     .Join(
                        productModels,
                        (product) => product.ProductID,
                        (SOD) => SOD.ProductId,
                        (SOD, product) => new SalesOrderDetailsModel()
                        {
                            SalesOrderID = SOD.SalesOrderID,
                            OrderQty = SOD.OrderQty,
                            UnitPrice = SOD.UnitPrice,
                            ProductID = SOD.ProductID,
                            Product = new ProductModel()
                            {
                                ProductId = product.ProductId,
                                Name = product.Name,
                                ProductNumber = product.ProductNumber
                            }
                        })
                     .ToList();

                   salesOrderHeaderModel.SalesOrderDetails = salesOrderDetailsProductData;
                   return salesOrderHeaderModel;
               })
               ?.ToList();

            return Task.FromResult<List<SalesOrderHeaderModel>>(salesOrderHeaderModels);
        }

        async Task<List<SalesOrderHeaderModel>> IRequestHandler<GetSalesOrderProductQuery, List<SalesOrderHeaderModel>>.Handle(GetSalesOrderProductQuery request, CancellationToken cancellationToken)
        {
            // Get Sales Order Header Data
            var getSalesOrderDataResultSet = await mediator.Send<List<SalesOrderHeaderModel>>(mapper.Map<GetSalesOrderIntegrationEvent>(request));

            // Get Product Id
            var productsId = await this.GetProductIdsAsync(getSalesOrderDataResultSet);

            // Get Product Data
            var getProductDataResultSet = await mediator.Send<List<ProductModel>>(new GetProductIntegrationEvent()
            {
                ProductIds = productsId
            });

            return await this.SalesOrderProductJoin(getSalesOrderDataResultSet, getProductDataResultSet);
        }
    }
}