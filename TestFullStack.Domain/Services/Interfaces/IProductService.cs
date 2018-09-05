using System.Collections.Generic;
using TestFullStack.Domain.Entities;


namespace TestFullStack.Domain.Services.Interfaces
{
    public interface IProductService
    {
        List<Product> Get(List<long> ids);
        void Save(Product product);
        void Remove(long id);
    }
}
