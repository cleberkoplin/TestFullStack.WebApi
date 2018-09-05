using System.Collections.Generic;
using TestFullStack.Domain.DTOs;
using TestFullStack.Domain.Entities;
using TestFullStack.Domain.Injection;
using TestFullStack.Domain.Repositories.Interfaces;
using TestFullStack.Domain.Services.Interfaces;
using Xunit;
using System.Linq;

namespace TestFullStack.Test
{
    public class UnitTestOrder
    {
        IUserService userService;
        IProductService productService;
        IOrderService orderService;
        IRepository<Order> repository;
        

        public UnitTestOrder()
        {
            Start.BindKernel();
            userService = Kernel.Get<IUserService>();
            productService = Kernel.Get<IProductService>();
            orderService = Kernel.Get<IOrderService>();
            repository = Kernel.Get<IRepository<Order>>();
        }

        [Fact]
        public void TestSave()
        {

            var user = new User();
            user.Name = "Fake Name";
            user.Password = "blablabla";
            user.Username = "fake";
            userService.Save(user);
            
            var product1 = new Product();
            product1.Name = "Fake Product 1";
            product1.Price = 100;
            productService.Save(product1);

            var product2 = new Product();
            product2.Name = "Fake Product 2";
            product2.Price = 200;
            productService.Save(product2);

            var idsProduct = new List<long> { product1.Id, product2.Id };

            var orderRequest = new OrderRequest();
            orderRequest.IdsProducts = idsProduct;
            orderRequest.IdUser = user.Id;

            orderService.Save(orderRequest);

            Order order = repository.GetAll().FirstOrDefault();
            
            //Assert.True(order != null && order.Items.Count() == 2 && order.User.Id == user.Id, "Save Order Ok!");
        }
        
       
    }
}
 