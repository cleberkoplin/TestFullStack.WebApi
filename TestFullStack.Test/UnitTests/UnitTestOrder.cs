using System.Collections.Generic;
using TestFullStack.Domain.DTOs;
using TestFullStack.Domain.Entities;
using TestFullStack.Domain.Injection;
using TestFullStack.Domain.Repositories.Interfaces;
using TestFullStack.Domain.Services.Interfaces;
using Xunit;
using System.Linq;
using TestFullStack.Domain.Services;
using TestFullStack.Domain.Utils;

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
            var kernel = Start.BindKernel();
            
            userService = kernel.Get<IUserService>();
            productService = kernel.Get<IProductService>();
            orderService = kernel.Get<IOrderService>();
            repository = kernel.Get<IRepository<Order>>();
        }

        [Fact]
        public void TestSave()
        {

            var user = new User();
            user.Name = "Fake Name Product";
            user.Password = CryptoTools.ComputeHashMd5("blablablaProduct");
            user.Username = "fakeProduct";
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

            Order order = repository.GetAll(true).FirstOrDefault();
            
            Assert.True(order != null && order.Items.Count() == 2 && order.User.Id == user.Id, "Save Order Ok!");
        }
        
       
    }
}
 