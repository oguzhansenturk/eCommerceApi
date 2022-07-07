using eCommerceApi.Application.Repositories;
using eCommerceApi.Domain.Entities;
using eCommerceApi.Persistence.Contexts;

namespace eCommerceApi.Persistence.Repositories;

public class CustomerWriteRepository : WriteRepository<Customer>,ICustomerWriteRepository
{
    public CustomerWriteRepository(eCommerceApiDbContext context) : base(context)
    {
    }
}