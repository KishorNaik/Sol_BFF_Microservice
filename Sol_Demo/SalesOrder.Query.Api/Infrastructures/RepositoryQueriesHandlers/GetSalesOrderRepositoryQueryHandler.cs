using Framework.SqlClient.Helper;
using MediatR;
using SalesOrder.Query.Api.Infrastructures.Abstracts;
using SalesOrder.Query.Api.Infrastructures.RepositoryQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using SalesOrder.Query.Api.Infrastructures.ResultSets;
using System.Data;
using SalesOrderProduct.Models.Shared.Models;

namespace SalesOrder.Query.Api.Infrastructures.RepositoryQueriesHandlers
{
    public sealed class GetSalesOrderRepositoryQueryHandler : SalesOrderRepositoryQueryAbstract, IRequestHandler<GetSalesOrderRepositoryQuery, IReadOnlyList<SalesOrderHeaderModel>>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public GetSalesOrderRepositoryQueryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task<IReadOnlyList<SalesOrderHeaderModel>> IRequestHandler<GetSalesOrderRepositoryQuery, IReadOnlyList<SalesOrderHeaderModel>>.Handle(GetSalesOrderRepositoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var dyanamicParameterTask = base.GetParameterAsync("Get-SalesOrder", request);

                var results =
                      this
                      .sqlClientDbProvider
                      .DapperBuilder
                      .OpenConnection(sqlClientDbProvider.GetConnection())
                      .Parameter(async () => await dyanamicParameterTask)
                      .Command(async (dbConnection, dynamicParameter) =>
                      {
                          var getSalesOrderData =
                                (
                                    await
                                    dbConnection
                                    ?.QueryAsync<GetSalesOrdersResultSet>("uspGetSalesOrderData", dynamicParameter, commandType: CommandType.StoredProcedure)
                                );

                          var getAggrgateSaleData =
                           getSalesOrderData
                           ?.AsParallel()
                           ?.AsSequential()
                           ?.GroupBy((getSalesOrderResultSetGroup) => new
                           {
                               getSalesOrderResultSetGroup.SalesOrderID,
                               getSalesOrderResultSetGroup.SalesOrderNumber,
                               getSalesOrderResultSetGroup.PurchaseOrderNumber,
                               getSalesOrderResultSetGroup.OrderDate
                           })
                           ?.Select((getSalesOrderResultSet) => new SalesOrderHeaderModel()
                           {
                               SalesOrderID = getSalesOrderResultSet.Key.SalesOrderID,
                               SalesOrderNumber = getSalesOrderResultSet.Key.SalesOrderNumber,
                               PurchaseOrderNumber = getSalesOrderResultSet.Key.PurchaseOrderNumber,
                               OrderDate = getSalesOrderResultSet.Key.OrderDate,
                               SalesOrderDetails =
                                                         getSalesOrderData
                                                         ?.AsParallel()
                                                         ?.AsSequential()
                                                         ?.Where((getSalesOrderResultSetWhere) => getSalesOrderResultSetWhere.SalesOrderID == getSalesOrderResultSet.Key.SalesOrderID)
                                                         ?.Select((getSelesOrderResultSetSelect) => new SalesOrderDetailsModel()
                                                         {
                                                             SalesOrderID = getSelesOrderResultSetSelect.SalesOrderID,
                                                             ProductID = getSelesOrderResultSetSelect.ProductID,
                                                             OrderQty = getSelesOrderResultSetSelect.OrderQty,
                                                             UnitPrice = getSelesOrderResultSetSelect.UnitPrice
                                                         })
                                                         ?.ToList()
                           })
                           ?.ToList()
                           ?.AsReadOnly();

                          return getAggrgateSaleData;
                      })
                      .ResultAsync<IReadOnlyList<SalesOrderHeaderModel>>();

                return results;
            }
            catch
            {
                throw;
            }
        }
    }
}