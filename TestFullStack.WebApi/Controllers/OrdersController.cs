using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TestFullStack.Domain.DTOs;
using TestFullStack.Domain.Entities;
using TestFullStack.Domain.Services.Interfaces;
using TestFullStack.Domain.Utils;

namespace TestFullStack.WebApi.Controllers
{


    [Route("[controller]")]
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

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                var details = new List<ItemOrderDetails>();
                foreach(ItemOrder item in _orderService.Get(id).Items)
                {
                    var detail = new ItemOrderDetails();
                    detail.Price = item.Price;
                    detail.Quantity = item.Quantity;
                    detail.ProductName = item.Product.Name;
                    details.Add(detail);
                }

                return Ok(details);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }


    }
}
