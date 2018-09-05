using Microsoft.EntityFrameworkCore;
using System;
using TestFullStack.Domain.Base;
using TestFullStack.Domain.Repositories.Interfaces;

namespace TestFullStack.Domain.Repositories
{
    public class Repository<T> : QueryRepository<T>, IRepository<T> where T : EntityBase
    {
        public Repository(DbContext context) : base(context)
        {

        }
        
        public void Insert(T entity)
        {
            entity.CreateDateTime = DateTime.Now;
            Entities.Add(entity);
        }

        public void Update(T entity)
        {
            entity.UpdateDateTime = DateTime.Now;
            Context.Entry<T>(entity).State = EntityState.Modified;
        }

        public void Save(T entity)
        {
            if (entity.Id == 0)
            {
                Insert(entity);
            }
            else
            {
                Update(entity);
            }
        }

        public void Delete(T entity)
        {
            Entities.Remove(entity);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

    }
}
