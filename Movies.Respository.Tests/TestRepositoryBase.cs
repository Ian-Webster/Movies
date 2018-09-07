using Microsoft.EntityFrameworkCore;
using System;

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

    }
}
