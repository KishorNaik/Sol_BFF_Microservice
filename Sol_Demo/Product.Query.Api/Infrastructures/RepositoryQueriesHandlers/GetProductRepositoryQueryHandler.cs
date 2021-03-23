using Framework.SqlClient.Helper;
using MediatR;
using Product.Query.Api.Infrastructures.Abstracts;
using Product.Query.Api.Infrastructures.RepositoryQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using SalesOrderProduct.Models.Shared.Models;

namespace Product.Query.Api.Infrastructures.RepositoryQueriesHandlers
{
    public sealed class GetProductRepositoryQueryHandler : ProductRepositoryQueryAbstract, IRequestHandler<GetProductRepositoryQuery, IReadOnlyList<ProductModel>>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public GetProductRepositoryQueryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task<IReadOnlyList<ProductModel>> IRequestHandler<GetProductRepositoryQuery, IReadOnlyList<ProductModel>>.Handle(GetProductRepositoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.GetParameterAsync("Get-Products", request);

                var results =
                        sqlClientDbProvider
                        .DapperBuilder
                        .OpenConnection(sqlClientDbProvider.GetConnection())
                        .Parameter(async () => await dynamicParameterTask)
                        .Command(async (dbConnection, dynamicParameter) =>
                        {
                            var procResult =
                                    (await
                                    dbConnection
                                    ?.QueryAsync<ProductModel>(sql: "uspGetProductData", param: dynamicParameter, commandType: CommandType.StoredProcedure)
                                    )
                                    ?.ToList()
                                    ?.AsReadOnly();

                            return procResult;
                        })
                        .ResultAsync<IReadOnlyList<ProductModel>>();

                return results;
            }
            catch
            {
                throw;
            }
        }
    }
}