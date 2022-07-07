using eCommerceApi.Application.Repositories;
using eCommerceApi.Domain.Entities;
using eCommerceApi.Persistence.Contexts;

namespace eCommerceApi.Persistence.Repositories;

public class OrderWriteRepository: WriteRepository<Order>,IOrderWriteRepository
{
    public OrderWriteRepository(eCommerceApiDbContext context) : base(context)
    {
    }
}