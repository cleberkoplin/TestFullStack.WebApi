using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TestFullStack.Domain.DTOs;
using TestFullStack.Domain.Entities;
using TestFullStack.Domain.Services.Interfaces;
using TestFullStack.Domain.Utils;
using TestFullStack.WebApi.Authorization;

namespace TestFullStack.WebApi.Controllers
{

    [UserAuthorization]
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

        /// <summary>
        /// Registers the order
        /// </summary>
        /// <param name="orderRequest">Order with ids of related product</param>
        /// <returns>Ok</returns>
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

        /// <summary>
        /// Search to orders
        /// </summary>
        /// <param name="filterOrderRequest">Filters by price range and date interval</param>
        /// <returns>All orders from logged user</returns>
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

        /// <summary>
        /// Get the details order
        /// </summary>
        /// <param name="id">Id of order</param>
        /// <returns>Details with items order, price, quantity and product name</returns>
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
