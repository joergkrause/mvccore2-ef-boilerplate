using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using JoergIsAGeek.Workshop.DataAccessLayer;
using JoergIsAGeek.Workshop.DomainModel;
using System.Linq.Expressions;
using JoergIsAGeek.Workshop.DomainModel.Abstracts;
using System.Threading.Tasks;
using JoergIsAGeek.Workshop.DataAccessLayer.ControlModels;

namespace JoergIsAGeek.Workshop.Repository
{
    public class GenericAsyncRepository<T> : IGenericRepository<T> where T : EntityBase
    {

        private readonly PersonalManagerContext context;

        public GenericAsyncRepository(PersonalManagerContext context)
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

        public async Task<T> Find(int id) {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> Read(Expression<Func<T, bool>> predicate)
        {
            var context = Context;
            var result = await context.Set<T>().Where(predicate).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<U>> FilterRead<U>(Expression<Func<U, bool>> predicate) where U : T
        {
            var context = Context;
            var result = await context.Set<U>().Where(predicate).OfType<U>().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<U>> FilterRead<U>(Expression<Func<U, bool>> predicate, params Expression<Func<U, object>>[] paths) where U : T
        {
            var context = Context;
            var result = context.Set<U>().Where(predicate);
            foreach (var path in paths)
            {
                result = result.Include(path);
            }
            return await result.OfType<U>().ToListAsync();
        }

        public async Task<IEnumerable<T>> Read(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths)
        {
            var context = Context;
            var result = context.Set<T>().Where(predicate);
            foreach (var path in paths)
            {
                result = result.Include(path);
            }
            return await result.ToListAsync();
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicate = null)
        {
            return await Context.Set<T>().CountAsync(predicate);
        }

        public async Task<ErrorModel> InsertOrUpdate(T model, string userName)
        {
            var context = Context;
            context.Entry(model).State = model.Id == 0 ? EntityState.Added : EntityState.Modified;
            return await context.SaveAsync(userName);
        }

        public async Task<ErrorModel> Delete(T model)
        {
            var context = Context;
            context.Entry(model).State = EntityState.Deleted;
            return await context.SaveAsync();
        }


    }
}
