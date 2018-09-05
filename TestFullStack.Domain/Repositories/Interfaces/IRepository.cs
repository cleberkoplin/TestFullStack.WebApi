using TestFullStack.Domain.Base;

namespace TestFullStack.Domain.Repositories.Interfaces
{
    public interface IRepository<T> : IQueryRepository<T> where T : IEntity
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save(T entity);
        void Save();
        
    }
}
