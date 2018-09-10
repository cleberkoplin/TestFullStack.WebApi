using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestFullStack.Domain.Entities;
using TestFullStack.Domain.Services.Interfaces;
using TestFullStack.WebApi.Authorization;

namespace TestFullStack.WebApi.Controllers
{

    [UserAuthorization]
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Save new product
        /// </summary>
        /// <param name="product">Product Object</param>
        /// <returns>Ok</returns>
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            try
            {
                _productService.Save(product);
                return Ok(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove product
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Ok</returns>
        [HttpPost]
        [Route("{id}")]
        public IActionResult Post(long id)
        {
            try
            {
                _productService.Remove(id);
                return Ok(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product">Data product to update</param>
        /// <returns>Ok</returns>
        [HttpPut]
        [Route("{id}")]
        public IActionResult Put([FromBody]Product product)
        {
            try
            {
                _productService.Save(product);
                return Ok(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all Product
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        [Route("all")]
        public async Task<List<Product>> GetAll()
        {
            try
            {
                return await _productService.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }


    }
}
