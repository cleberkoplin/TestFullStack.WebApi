using System;
using TestFullStack.Domain.Base;
using System.Linq;
using System.Linq.Expressions;

namespace TestFullStack.Domain.Repositories.Interfaces
{
    public interface IQueryRepository<T> where T : IEntity
    {
        T Get(long id);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
    }
}
