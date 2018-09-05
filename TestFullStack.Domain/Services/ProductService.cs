﻿using System.Collections.Generic;
using TestFullStack.Domain.Entities;
using TestFullStack.Domain.Repositories.Interfaces;
using TestFullStack.Domain.Services.Interfaces;
using System.Linq;

namespace TestFullStack.Domain.Services
{
    public class ProductService : IProductService
    {
        IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> Get(List<long> ids)
        {
            return _productRepository.Get(x => ids.Contains(x.Id)).ToList();
        }
        
        public void Save(Product product)
        {
            if (product.Id == 0)
            {
                _productRepository.Insert(product);
            }
            else
            {
                _productRepository.Update(product);
            }
            
            _productRepository.Save();
        }

        public void Remove(long id)
        {
            var product  = _productRepository.Get(id);
            _productRepository.Delete(product);
            _productRepository.Save();
        }



    }
}
