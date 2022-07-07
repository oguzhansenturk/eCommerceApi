using eCommerceApi.Application.Repositories;
using eCommerceApi.Domain.Entities.Common;
using eCommerceApi.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eCommerceApi.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T: BaseEntity
{
    private readonly eCommerceApiDbContext _context;

    public WriteRepository(eCommerceApiDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();
    
    public async Task<bool> AddAsync(T model)
    {
      var entityEntry =  await Table.AddAsync(model);
      return entityEntry.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(List<T> data)
    {
        await Table.AddRangeAsync(data);
        return true;
    }

    public bool Remove(T data)
    {
        var entityEntry =  Table.Remove(data);
        return entityEntry.State == EntityState.Deleted;
    }

    public bool RemoveRange(List<T> data)
    {
        Table.RemoveRange(data);
        return true;
    }

    public async Task<bool> RemoveAsync(string id)
    {
        var model = await Table.FirstOrDefaultAsync(i => i.Id == Guid.Parse(id));
        return Remove(model);
    }

    public bool Update(T model)
    {
        var entityEntry = Table.Update(model);
        return entityEntry.State == EntityState.Modified;
    }

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
}