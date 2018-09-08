using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using TestFullStack.Domain.Base;
using TestFullStack.Domain.Repositories.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TestFullStack.Domain.Repositories
{
    public class QueryRepository<T> : IQueryRepository<T> where T: EntityBase
    {
        protected DbContext Context { get; set; }
        protected DbSet<T> Entities { get; set; }

        public QueryRepository(DbContext context)
        {
            Context = context;
            Entities = Context.Set<T>();
        }

        public T Get(long id)
        {
            var result = Entities.Where(x => x.Id == id).AsQueryable();
            var entity = result.FirstOrDefault();
            Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            var result = Entities.Where(predicate).AsQueryable();
            return result;
        }

        public IQueryable<T> GetAll()
        {
            var result = Entities.AsQueryable();
            return result;
        }

        public Task<List<T>> GetAllAwaiter()
        {
            var result = Entities.ToListAsync();
            return result;
        }

    }
}
