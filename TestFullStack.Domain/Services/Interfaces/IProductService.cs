using System.Collections.Generic;
using System.Threading.Tasks;
using TestFullStack.Domain.Entities;


namespace TestFullStack.Domain.Services.Interfaces
{
    public interface IProductService
    {
        List<Product> Get(List<long> ids);
        void Save(Product product);
        void Remove(long id);
        Task<List<Product>> GetAll();
        Product Get(long id);
    }
}
