using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SodgeIt.Workshop.DataAccessLayer;
using SodgeIt.Workshop.DomainModel;
using System.Linq.Expressions;

namespace SodgeIt.Workshop.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {

        private readonly PersonalManagerContext context;

        public GenericRepository(PersonalManagerContext context)
        {
            this.context = context;
        }

        private PersonalManagerContext Context
        {
            get
            {
                return context;
            }
        }

        object _lock = new object();

        public IEnumerable<T> Read(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> result = null;
            // Nur zum Zeigen!
            lock (_lock)
            {
                var context = Context;
                result = context.Set<T>().Where(predicate);
            }
            return result;
        }

        public IEnumerable<U> FilterRead<U>(Expression<Func<U, bool>> predicate) where U : T
        {
            var context = Context;
            var result = context.Set<U>().Where(predicate).OfType<U>();
            return result;
        }

        public IEnumerable<U> FilterRead<U>(Expression<Func<U, bool>> predicate, params Expression<Func<U, object>>[] paths) where U : T
        {
            var context = Context;
            var result = context.Set<U>().Where(predicate);
            foreach (var path in paths)
            {
                result = result.Include(path);
            }
            return result.OfType<U>();
        }

        public IEnumerable<T> Read(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths)
        {
            var context = Context;
            var result = context.Set<T>().Where(predicate);
            foreach (var path in paths)
            {
                result = result.Include(path);
            }
            return result;
        }

        // Idee: 
        // public int Count(Expression<Func<T, bool>> predicate = null)
        // {
        //     return Context.Set<T>().Count(predicate);
        // }

        public bool InsertOrUpdate(T model)
        {
            var context = Context;
            context.Entry(model).State = model.Id == 0 ? EntityState.Added : EntityState.Modified;
            return context.SaveChanges() == 1;
        }

        public bool Delete(T model)
        {
            var context = Context;
            context.Entry(model).State = EntityState.Deleted;
            return context.SaveChanges() == 1;
        }


    }
}
