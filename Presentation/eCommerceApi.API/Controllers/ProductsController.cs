using System.Net;
using eCommerceApi.Application.Repositories;
using eCommerceApi.Application.RequestParameters;
using eCommerceApi.Application.ViewModels.Products;
using eCommerceApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll().Count();
            var products = _productReadRepository.GetAll(false).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).Skip(pagination.Page * pagination.Size).Take(pagination.Size);

            return Task.FromResult<IActionResult>(
                Ok(new
                {
                    totalCount, products
                }));
        }

        [HttpGet("{id}")]
        public Task<IActionResult> Get(string id)
        {
            var product = _productReadRepository.GetByIdAsync(id, false);
            return Task.FromResult<IActionResult>(Ok(product));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VM_Product_Create? product)
        {
            await _productWriteRepository.AddAsync(new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int) HttpStatusCode.Created);
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
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");
            if(!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            
            Request.Form.Files.ToList().ForEach(async file =>
            {
                if (file.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(uploadPath, fileName);
                    await using var stream = new FileStream(filePath, FileMode.Create,FileAccess.Write,FileShare.None,1024*1024,false);
                    await file.CopyToAsync(stream);
                    await stream.FlushAsync();
                }
            });
            
            return Ok();
        }
    }
}
