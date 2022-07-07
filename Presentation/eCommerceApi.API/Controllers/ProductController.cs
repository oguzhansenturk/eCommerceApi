using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;

        readonly private IOrderWriteRepository _orderWriteRepository;
        readonly private ICustomerWriteRepository _customerWriteRepository;
        
        public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository,IOrderWriteRepository orderWriteRepository,ICustomerWriteRepository customerWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            var custId = Guid.NewGuid();
            await _customerWriteRepository.AddAsync(new() {Id = custId, Name = "Oguz"});
            await _orderWriteRepository.AddAsync(new() {Description = "bla bla bla", Address = "Adresses"});
            _orderWriteRepository.SaveAsync();
        }
        
    }
}
