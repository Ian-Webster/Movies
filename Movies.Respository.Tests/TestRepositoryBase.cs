using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using DataAccess.Repository;
using Microsoft.Extensions.Logging.Abstractions;

namespace Movies.Repository.Tests
{
    public class TestRepositoryBase
    {

        protected DbContextOptionsBuilder<Context> options;

        protected void Setup()
        {
            options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()); //ensure we get a unique database for each test
        }

        protected Context GetContext()
        {
            return new Context(options.Options);
        }

        protected UnitOfWork<Context> GetUnitOfWork()
        {
            return new UnitOfWork<Context>(GetContext(), new NullLoggerFactory());
        }

        protected CancellationToken GetCancellationToken()
        {
            return new CancellationToken();
        }

    }
}
