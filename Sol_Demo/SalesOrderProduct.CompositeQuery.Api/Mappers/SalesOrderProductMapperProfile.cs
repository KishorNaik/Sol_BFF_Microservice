using AutoMapper;
using SalesOrderProduct.CompositeQuery.Api.Application.DomainQueries.Queries;
using SalesOrderProduct.CompositeQuery.Api.Application.IntegrationEvents.Events;
using SalesOrderProduct.Models.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrderProduct.CompositeQuery.Api.Mappers
{
    public class SalesOrderProductMapperProfile : Profile
    {
        public SalesOrderProductMapperProfile()
        {
            base.CreateMap<GetSalesOrderIntegrationEvent, SalesOrderHeaderModel>()
                .ForPath((dest) => dest.Pagination.PageNumber, (opt) => opt.MapFrom((src) => src.PageNumber))
                .ForPath((dest) => dest.Pagination.RowsOfPage, (opt) => opt.MapFrom((src) => src.Rows));

            base.CreateMap<GetProductIntegrationEvent, ProductModel>();

            base.CreateMap<GetSalesOrderProductQuery, GetSalesOrderIntegrationEvent>()
                .ForMember((dest) => dest.PageNumber, (opt) => opt.MapFrom((src) => src.Pagination.PageNumber))
                .ForMember((dest) => dest.Rows, (opt) => opt.MapFrom((src) => src.Pagination.RowsOfPage));
        }
    }
}