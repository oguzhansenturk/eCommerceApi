using System;
using eCommerceApi.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eCommerceApi.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<eCommerceApiDbContext>
    {
        //dotnet ef migrations add mig_1
        //dotnet ef database update

        public eCommerceApiDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<eCommerceApiDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}

