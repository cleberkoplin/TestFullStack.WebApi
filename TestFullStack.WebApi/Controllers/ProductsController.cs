using System;
using Microsoft.AspNetCore.Mvc;
using TestFullStack.Domain.Entities;
using TestFullStack.Domain.Services.Interfaces;

namespace TestFullStack.WebApi.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        
        
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


    }
}
