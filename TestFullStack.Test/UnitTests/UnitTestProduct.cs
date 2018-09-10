using System.Collections.Generic;
using TestFullStack.Domain.DTOs;
using TestFullStack.Domain.Entities;
using TestFullStack.Domain.Injection;
using TestFullStack.Domain.Repositories.Interfaces;
using TestFullStack.Domain.Services.Interfaces;
using Xunit;
using System.Linq;
using TestFullStack.Domain.Services;

namespace TestFullStack.Test
{
    public class UnitTestProduct
    {
        IProductService productService;
        IRepository<Product> repository;
        

        public UnitTestProduct()
        {
            var kernel = Start.BindKernel();
           
            productService = kernel.Get<IProductService>();
            repository = kernel.Get<IRepository<Product>>();
        }

        [Fact]
        public void TestInsert()
        {
            
            var product1 = new Product();
            product1.Name = "Fake Product Insert";
            product1.Price = 100;
            productService.Save(product1);

            Assert.True(repository.Get(product1.Id) != null, "Produto Inserido!");
        }

        [Fact]
        public void TestUpdate()
        {

            var product1 = new Product();
            product1.Name = "Fake Product Update";
            product1.Price = 100;

            productService.Save(product1);

            product1.Name = "Altered";
            productService.Save(product1);

            Product productTest = productService.Get(product1.Id);

            Assert.True(productTest.Name.Equals("Altered"), "Produto Alterado!");
        }

        [Fact]
        public void TestDelete()
        {

            var product1 = new Product();
            product1.Name = "Fake Product Delete";
            product1.Price = 200;

            productService.Save(product1);

            productService.Remove(product1.Id);

            Assert.True(productService.Get(product1.Id) == null, "Produto Removido");
        }


    }
}
 