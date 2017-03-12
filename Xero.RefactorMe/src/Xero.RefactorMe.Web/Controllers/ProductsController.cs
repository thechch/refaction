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
        private readonly IProductRepository _productsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProductsController(IProductRepository productRepository, IMapper mapper, ILogger<ProductsController> logger)
        {
            _productsRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        //GET /products
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productsRepository.GetAll();

            if (!products.Any())
            {
                return NotFound();
            }

            var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products).ToList();

            return new ObjectResult(mappedProducts);
        }

        //GET /products/Apple%20iPhone%206S
        [HttpGet("{name}")]
        public IActionResult SearchByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            var productsByName = _productsRepository.GetByName(name);

            if (!productsByName.Any())
            {
                return NotFound();
            }

            var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productsByName);

            return new ObjectResult(productsByName);
        }

        //GET /products/de1287c0-4b15-4a7b-9d8a-dd21b3cafec3
        [HttpGet("{id:Guid}", Name = "GetProduct")]
        public IActionResult GetProduct(Guid Id)
        {
            var product = _productsRepository.GetSingle(Id);

            if(product == null) {
                return NotFound();
            }

            var mappedProduct = _mapper.Map<Product, ProductViewModel>(product);

            return new ObjectResult(mappedProduct);
        }

        //POST /products/
        [HttpPost]
        public IActionResult Create([FromBody]ProductViewModel model)
        {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var newProduct = _mapper.Map<ProductViewModel, Product>(model);
            _productsRepository.Add(newProduct);

            try 
            {
                _productsRepository.Commit();
            }
            catch(DbUpdateException e)
            {
                _logger.LogCritical(e.Message);
                throw e;
            }

            var newModel = _mapper.Map<Product, ProductViewModel>(newProduct);
            return CreatedAtRoute("GetProduct", new { id = newModel.ProductId}, newModel);
        }

        //PUT /products/de1287c0-4b15-4a7b-9d8a-dd21b3cafec3
        [HttpPut("{id:Guid}")]
        public IActionResult Update(Guid id, [FromBody]ProductViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = _productsRepository.GetSingle(id);
            if(product == null)
            {
                return NotFound();
            }

            var updatedProduct = _mapper.Map<ProductViewModel, Product>(model, product);
            _productsRepository.Update(updatedProduct);

            try 
            {
                _productsRepository.Commit();
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
            var product = _productsRepository.GetSingle(id);
            if(product == null)
            {
                return NotFound();
            }
             _productsRepository.Delete(product);
            try
            {
                _productsRepository.Commit();
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