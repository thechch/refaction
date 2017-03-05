using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Xero.RefactorMe.Data.Abstract;
using Xero.RefactorMe.Model;
using Xero.RefactorMe.Web.ViewModels;

namespace Xero.RefactorMe.Web.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private IProductRepository _productRepository {get; set;}
        private IMapper _mapper;
        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        //GET api/products
        [HttpGet]
        public IEnumerable<ProductViewModel> GetAllProducts() {
            var products = _productRepository.GetAll();
            var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);

            return mappedProducts;
        }

        //GET api/products/iPhone%206s
        [HttpGet("{name}")]
        public IActionResult GetProductsByName(string name) {
            var productsByName = _productRepository.AllIncluding(p => p.Name == name);
            var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productsByName);
            return Ok(mappedProducts);
        }
    }
}