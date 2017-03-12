using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xero.RefactorMe.Data.Abstract;
using Xero.RefactorMe.Model;
using Xero.RefactorMe.Web.ViewModels;

namespace Xero.RefactorMe.Web.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProductsController(IProductRepository productRepository, IMapper mapper, ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        //GET /products
        [HttpGet]
        public IEnumerable<ProductViewModel> GetAll()
        {
            //Should be _productsService.GetAll();
            var products = _productRepository.GetAll();
            var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products).ToList();

            if (!(mappedProducts.Count > 0))
            {
                return new List<ProductViewModel>();
            }

            return mappedProducts;
        }

        //GET /products/Apple%20iPhone%206S
        [HttpGet("{name}")]
        public IActionResult SearchByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            var productsByName = _productRepository.GetByName(name);

            if (!productsByName.Any())
            {
                return NotFound();
            }

            var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productsByName);

            return Ok(productsByName);
        }

        //GET /products/de1287c0-4b15-4a7b-9d8a-dd21b3cafec3
        [HttpGet("{id:Guid}", Name = "GetProduct")]
        public IActionResult GetProduct(Guid Id)
        {
            var product = _productRepository.GetSingle(Id);

            if(product == null) {
                return NotFound();
            }

            var mappedProduct = _mapper.Map<Product, ProductViewModel>(product);

            return Ok(mappedProduct);
        }

        //POST /products/
        [HttpPost]
        public IActionResult Create([FromBody]ProductViewModel model)
        {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var newProduct = _mapper.Map<ProductViewModel, Product>(model);
            _productRepository.Add(newProduct);

            try 
            {
                _productRepository.Commit();
            }
            catch(DbUpdateException e)
            {
                _logger.LogCritical(e.Message);
                throw e;
            }

            var updatedProduct = _mapper.Map<Product, ProductViewModel>(newProduct);
            return CreatedAtRoute("GetProduct", new { id = updatedProduct.ProductId}, updatedProduct);
        }

        //PUT /products/de1287c0-4b15-4a7b-9d8a-dd21b3cafec3
        [HttpPut("{id:Guid}")]
        public IActionResult Update(Guid id, [FromBody]ProductViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = _productRepository.GetSingle(id);
            if(product == null)
            {
                return NotFound();
            }

            var updatedProduct = _mapper.Map<ProductViewModel, Product>(model, product);
            _productRepository.Update(updatedProduct);

            try 
            {
                _productRepository.Commit();
            }
            catch(DbUpdateException e)
            {
                _logger.LogCritical(e.Message);
                throw e;
            }

            return new NoContentResult();
        }

         //DELETE /products/de1287c0-4b15-4a7b-9d8a-dd21b3cafec3
        [HttpDelete("{id:Guid}")]
        public StatusCodeResult Delete(Guid id)
        {
            var product = _productRepository.GetSingle(id);
            if(product == null)
            {
                return NotFound();
            }
             _productRepository.Delete(product);
            try
            {
                _productRepository.Commit();
            }
            catch(DbUpdateException e)
            {
                _logger.LogCritical(e.Message);
                throw e;
            } 
            
            return new NoContentResult();
        }
    }
}