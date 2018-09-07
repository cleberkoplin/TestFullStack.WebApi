using System;
using Microsoft.AspNetCore.Mvc;
using TestFullStack.Domain.DTOs;
using TestFullStack.Domain.Services.Interfaces;
using TestFullStack.Domain.Utils;

namespace TestFullStack.WebApi.Controllers
{


    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        
        [HttpPost]
        public IActionResult Post([FromBody]OrderRequest orderRequest)
        {
            try
            {
                //get id user by token autentication
                orderRequest.IdUser = HttpContext.GetToken().Id;
                _orderService.Save(orderRequest);
                return Ok(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Get([FromQuery]FilterOrderRequest filterOrderRequest)
        {
            try
            {
                return Ok(_orderService.Get(filterOrderRequest, HttpContext.GetToken()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                return Ok(_orderService.Get(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }


    }
}
