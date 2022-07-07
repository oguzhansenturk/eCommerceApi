using System.Net;
using eCommerceApi.Application.Repositories;
using eCommerceApi.Application.ViewModels.Products;
using eCommerceApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        
        public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
       
        }

        [HttpGet]
        public Task<IActionResult> Get()
        {
            var products = _productReadRepository.GetAll();
            return Task.FromResult<IActionResult>(Ok(products));
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VM_Product_Create? product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            await _productWriteRepository.AddAsync(new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
                
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }
        
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] VM_Product_Update? product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            
            var productToUpdate = await _productReadRepository.GetByIdAsync(product.Id);
            productToUpdate.Name = product.Name;
            productToUpdate.Price = product.Price;
            productToUpdate.Stock = product.Stock;
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}
