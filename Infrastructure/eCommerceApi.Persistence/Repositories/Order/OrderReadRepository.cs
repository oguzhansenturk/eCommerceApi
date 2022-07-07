using eCommerceApi.Application.Repositories;
using eCommerceApi.Domain.Entities;
using eCommerceApi.Persistence.Contexts;

namespace eCommerceApi.Persistence.Repositories;

public class OrderReadRepository : ReadRepository<Order>,IOrderReadRepository
{
    public OrderReadRepository(eCommerceApiDbContext context) : base(context)
    {
    }
}