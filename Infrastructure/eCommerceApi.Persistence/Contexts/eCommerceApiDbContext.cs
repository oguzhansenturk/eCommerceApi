using System;
using eCommerceApi.Domain.Entities;
using eCommerceApi.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApi.Persistence.Contexts
{
    public class eCommerceApiDbContext : DbContext
    {
        public eCommerceApiDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           var datas = ChangeTracker.Entries<BaseEntity>();

           foreach (var data in datas)
           {
               _ = data.State switch
               {
                   EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                   EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow
               };
           }
           return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

