using AutoMapper;
using Product.Query.Api.Applications.Queries;
using Product.Query.Api.Infrastructures.RepositoryQueries;
using SalesOrderProduct.Models.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Query.Api.Mappers
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            base.CreateMap<GetProductQuery, GetProductRepositoryQuery>();
        }
    }
}