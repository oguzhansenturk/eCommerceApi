using eCommerceApi.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApi.Application.Repositories;

public interface IRepository<T> where T : BaseEntity 
{
    DbSet<T> Table { get; }
}