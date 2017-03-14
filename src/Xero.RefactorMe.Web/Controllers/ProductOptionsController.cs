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
    public class ProductOptionsController : Controller
    {
        private readonly IProductOptionRepository _productOptionRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProductOptionsController(IProductOptionRepository productOptionRepository, IProductRepository productRepository, IMapper mapper, ILogger<ProductsController> logger)
        {
            _productOptionRepository = productOptionRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        //GET /products/de1287c0-4b15-4a7b-9d8a-dd21b3cafec3/options
        [HttpGet("products/{productId:Guid}/options")]
        public IActionResult GetOptions(Guid productId)
        {
            var options = _productOptionRepository.GetMultiple(s => s.ProductId == productId);

            if (options == null || !options.Any())
            {
                return NotFound();
            }

            var mappedOptions = _mapper.Map<IEnumerable<ProductOption>, IEnumerable<ProductOptionViewModel>>(options).ToList();

            return new ObjectResult(mappedOptions);
        }

        //GET /products/de1287c0-4b15-4a7b-9d8a-dd21b3cafec3/options/5c2996ab-54ad-4999-92d2-89245682d534
        [HttpGet("products/{productId:Guid}/options/{id:Guid}", Name = "GetOption")]
        public IActionResult GetOption(Guid productId, Guid id)
        {
            var options = _productOptionRepository.GetMultiple(s => s.ProductId == productId);
            if (options == null || !options.Any())
            {
                return NotFound();
            }

            var option = options.Where(p => p.Id == id).First();
            var mappedOption = _mapper.Map<ProductOption, ProductOptionViewModel>(option);

            return new ObjectResult(mappedOption);
        }

        //POST /products/de1287c0-4b15-4a7b-9d8a-dd21b3cafec3/options
        [HttpPost("products/{productId:Guid}/options")]
        public IActionResult Create(Guid productId, [FromBody]ProductOptionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _productRepository.GetSingle(productId);
            if (product == null)
            {
                return NotFound();
            }

            var newOption = _mapper.Map<ProductOptionViewModel, ProductOption>(model);
            newOption.ProductId = productId;
            newOption.Product = product;

            _productOptionRepository.Add(newOption);

            try
            {
                _productOptionRepository.Commit();
            }
            catch (DbUpdateException e)
            {
                _logger.LogCritical(e.Message);
                throw e;
            }

            var newModel = _mapper.Map<ProductOption, ProductOptionViewModel>(newOption);
            return CreatedAtRoute("GetOption", new { productId = productId, id = newModel.Id }, newModel);
        }

        //PUT /products/de1287c0-4b15-4a7b-9d8a-dd21b3cafec3/options/5c2996ab-54ad-4999-92d2-89245682d534
        [HttpPut("products/{productId:Guid}/options/{id:Guid}")]
        public IActionResult Update(Guid productId, Guid id, [FromBody]ProductOptionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var options = _productOptionRepository.GetMultiple(s => s.ProductId == productId);
            if (options == null || !options.Any())
            {
                return NotFound();
            }

            var option = options.Where(p => p.Id == id).First();

            var updatedOption = _mapper.Map<ProductOptionViewModel, ProductOption>(model, option);
            option.ProductId = productId;

            _productOptionRepository.Update(updatedOption);

            try
            {
                _productOptionRepository.Commit();
            }
            catch (DbUpdateException e)
            {
                _logger.LogCritical(e.Message);
                throw e;
            }

            return new NoContentResult();
        }

        //DELETE /products/de1287c0-4b15-4a7b-9d8a-dd21b3cafec3/options/5c2996ab-54ad-4999-92d2-89245682d534
        [HttpDelete("products/{productId:Guid}/options/{id:Guid}")]
        public StatusCodeResult Delete(Guid id)
        {
            var option = _productOptionRepository.GetSingle(id);
            if (option == null)
            {
                return NotFound();
            }
            _productOptionRepository.Delete(option);
            try
            {
                _productOptionRepository.Commit();
            }
            catch (DbUpdateException e)
            {
                _logger.LogCritical(e.Message);
                throw e;
            }

            return new NoContentResult();
        }
    }
}