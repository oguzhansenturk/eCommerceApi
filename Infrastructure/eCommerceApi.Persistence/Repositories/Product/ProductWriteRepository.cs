using eCommerceApi.Application.Repositories;
using eCommerceApi.Domain.Entities;
using eCommerceApi.Persistence.Contexts;

namespace eCommerceApi.Persistence.Repositories;

public class ProductWriteRepository : WriteRepository<Product>,IProductWriteRepository
{
    public ProductWriteRepository(eCommerceApiDbContext context) : base(context)
    {
    }
}