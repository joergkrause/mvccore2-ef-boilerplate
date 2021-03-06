﻿using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using JoergIsAGeek.Workshop.DataAccessLayer;
using JoergIsAGeek.Workshop.DomainModel;
using System.Linq.Expressions;
using JoergIsAGeek.Workshop.DomainModel.Abstracts;
using JoergIsAGeek.Workshop.DataAccessLayer.ControlModels;

namespace JoergIsAGeek.Workshop.Repository
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

        public T Find(int id) {
            return Context.Set<T>().Find(id);
        }

        public IEnumerable<T> Read(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> result = null;
            var context = Context;
            result = context.Set<T>().Where(predicate);
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

        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            return Context.Set<T>().Count(predicate);
        }

        public ErrorModel InsertOrUpdate(T model, string userName)
        {
            var context = Context;
            context.Entry(model).State = model.Id == 0 ? EntityState.Added : EntityState.Modified;
            return context.Save(userName);
        }

        public ErrorModel Delete(T model)
        {
            var context = Context;
            context.Entry(model).State = EntityState.Deleted;
            return context.Save();
        }


    }
}
