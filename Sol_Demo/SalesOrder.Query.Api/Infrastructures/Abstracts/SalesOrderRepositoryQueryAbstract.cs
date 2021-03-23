using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.CompilerServices;
using SalesOrderProduct.Models.Shared.Models;

namespace SalesOrder.Query.Api.Infrastructures.Abstracts
{
    public abstract class SalesOrderRepositoryQueryAbstract
    {
        protected Task<DynamicParameters> GetParameterAsync(string command, SalesOrderHeaderModel salesOrderHeaderModel)
        {
            return Task.Run(() =>
            {
                var dynamicParameter = new DynamicParameters();

                dynamicParameter.Add("@Command", command, DbType.String, ParameterDirection.Input);
                dynamicParameter.Add("@PageNumber", salesOrderHeaderModel.Pagination.PageNumber, DbType.Int32, ParameterDirection.Input);
                dynamicParameter.Add("@RowsOfPage", salesOrderHeaderModel.Pagination.RowsOfPage, DbType.Int32, ParameterDirection.Input);

                return dynamicParameter;
            });
        }
    }
}