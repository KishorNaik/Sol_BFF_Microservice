using AutoMapper;
using MassTransit.Registration;
using SalesOrder.Query.Api.Applications.DomainQueries.Queries;
using SalesOrder.Query.Api.Infrastructures.RepositoryQueries;
using SalesOrderProduct.Models.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.Query.Api.Mappers
{
    public class SalesOrderMapperProfile : Profile
    {
        public SalesOrderMapperProfile()
        {
            base.CreateMap<GetSalesOrderQuery, GetSalesOrderRepositoryQuery>()
                  .ForPath((dest) => dest.Pagination.PageNumber, (opt) => opt.MapFrom((src => src.PageNumber)))
                  .ForPath((dest) => dest.Pagination.RowsOfPage, (opt) => opt.MapFrom((src) => src.Rows));
        }
    }
}