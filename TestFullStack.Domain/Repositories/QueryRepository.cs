using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using TestFullStack.Domain.Base;
using TestFullStack.Domain.Repositories.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;

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

        public T Get(long id, bool loadFirstChild = false)
        {
            var result = Entities.Where(x => x.Id == id).AsQueryable();

            // Incluindo automaticamente o primeiro nível de associações e coleções
            if (loadFirstChild)
            {
                var include = GetInclude(typeof(T));
                foreach (var item in include)
                    result = result.Include(item).AsQueryable();
            }

            var entity = result.FirstOrDefault();
            Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, bool loadFirstChild = false)
        {
            var result = Entities.Where(predicate).AsQueryable();

            // Incluindo automaticamente o primeiro nível de associações e coleções
            if (loadFirstChild)
            {
                var include = GetInclude(typeof(T));
                foreach (var item in include)
                    result = result.Include(item).AsQueryable();
            }

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

        private static IEnumerable<string> GetInclude(Type type)
        {
            var lista = new List<string>();

            var props = type.GetProperties();
            foreach (var prop in props)
            {
                // Associações
                if (typeof(EntityBase).IsAssignableFrom(prop.PropertyType))
                    lista.Add(prop.Name);

                // Coleções
                if (IsCollection(prop))
                    lista.Add(prop.Name);
            }

            return lista.ToArray();
        }

        private static bool IsCollection(PropertyInfo prop)
        {
            var propType = prop.PropertyType;
            if (!propType.IsGenericType)
                return false;

            var genericType = propType.GetGenericTypeDefinition();
            return genericType == typeof(IList<>);
        }

    }
}
