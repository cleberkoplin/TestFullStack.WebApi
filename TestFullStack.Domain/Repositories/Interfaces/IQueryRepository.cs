using System;
using TestFullStack.Domain.Base;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TestFullStack.Domain.Repositories.Interfaces
{
    public interface IQueryRepository<T> where T : IEntity
    {
        T Get(long id);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        Task<List<T>> GetAllAwaiter();
    }
}
