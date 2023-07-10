
using Microsoft.Extensions.Logging;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data;

namespace ShowcaseProject.Products.Application.Queries.Products;

public sealed class ProductQueryFactory : QueryFactory
{
    public ProductQueryFactory(IDbConnection connection, Compiler compiler, ILogger<ProductQueryFactory>? logger)
        : base(connection, compiler)
    {
        if (logger is not null)
        {
            Logger = compiledQuery =>
            {
                logger.LogDebug("Raw SQL Product Query: {sqlQuery}", compiledQuery.ToString());
            };
        }
    }
}