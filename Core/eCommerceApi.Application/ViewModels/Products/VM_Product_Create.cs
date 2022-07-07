using eCommerceApi.Domain.Entities;

namespace eCommerceApi.Application.ViewModels.Products;

public class VM_Product_Create
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public float Price { get; set; }
    public ICollection<Order> Orders { get; set; }
}