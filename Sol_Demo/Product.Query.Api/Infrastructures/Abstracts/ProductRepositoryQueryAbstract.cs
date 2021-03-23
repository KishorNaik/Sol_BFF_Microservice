using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using SalesOrderProduct.Models.Shared.Models;

namespace Product.Query.Api.Infrastructures.Abstracts
{
    public abstract class ProductRepositoryQueryAbstract
    {
        protected Task<DynamicParameters> GetParameterAsync(string command, ProductModel productDTO = null)
        {
            return Task.Run(() =>
            {
                var dynamicParameter = new DynamicParameters();

                dynamicParameter.Add("@Command", command, DbType.String, ParameterDirection.Input);
                dynamicParameter.Add("@ProductIds", productDTO?.ProductIds, DbType.String, ParameterDirection.Input);

                return dynamicParameter;
            });
        }
    }
}