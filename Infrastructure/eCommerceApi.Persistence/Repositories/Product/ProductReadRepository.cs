using eCommerceApi.Application.Repositories;
using eCommerceApi.Domain.Entities;
using eCommerceApi.Persistence.Contexts;

namespace eCommerceApi.Persistence.Repositories;

public class ProductReadRepository : ReadRepository<Product>,IProductReadRepository
{
    public ProductReadRepository(eCommerceApiDbContext context) : base(context)
    {
    }
}