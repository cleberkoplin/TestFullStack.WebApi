using System.Collections.Generic;
using TestFullStack.Domain.Entities;
using TestFullStack.Domain.Repositories.Interfaces;
using TestFullStack.Domain.Services.Interfaces;
using System.Linq;
using TestFullStack.Domain.DTOs;
using TestFullStack.Domain.Utils;

namespace TestFullStack.Domain.Services
{
    public class OrderService : IOrderService
    {
        IRepository<Order> _orderRepository;
        IUserService _userService;
        IProductService _productService;

        public OrderService(IRepository<Order> orderRepository, IUserService userService, IProductService productService)
        {
            _orderRepository = orderRepository;
            _userService = userService;
            _productService = productService;
        }

        
        public void Save(OrderRequest orderRequest)
        {

            var user = _userService.Get(orderRequest.IdUser);
            var products = _productService.Get(orderRequest.IdsProducts);

            var order = new Order();
            var items = new List<ItemOrder>();
            decimal totalPrice = 0;
            foreach(Product product in products)
            {
                var item = new ItemOrder();
                item.ProductId = product.Id;
                item.Price = product.Price;
                item.Quantity = orderRequest.IdsProducts.Where(x => x == product.Id).Count();
                totalPrice += (item.Price * item.Quantity);
                items.Add(item);
            }

            order.Items = items;
            order.Price = totalPrice;
            order.UserId = user.Id;

            _orderRepository.Insert(order);
            _orderRepository.Save();

        }

        
        public List<Order> Get(FilterOrderRequest filterOrderRequest, Token token)
        {
            var predicate = PredicateBuilder.True<Order>();

            predicate = predicate.And(x => x.User.Id == token.Id);

            if (filterOrderRequest.StartDate != null)
                predicate = predicate.And(x => x.CreateDateTime >= filterOrderRequest.StartDate);

            if (filterOrderRequest.EndDate != null)
                predicate = predicate.And(x => x.CreateDateTime <= filterOrderRequest.EndDate);

            if (filterOrderRequest.StartPrice > 0)
                predicate = predicate.And(x => x.Price >= filterOrderRequest.StartPrice);

            if (filterOrderRequest.EndPrice > 0)
                predicate = predicate.And(x => x.Price <= filterOrderRequest.EndPrice);

            return _orderRepository.Get(predicate).ToList();
        }

        public Order Get(long id)
        {
            Order order = _orderRepository.Get(id, true);
            
            foreach(ItemOrder item in order.Items)
            {
                item.Product = _productService.Get(item.ProductId);
            }

            return order;
        }


    }
}
