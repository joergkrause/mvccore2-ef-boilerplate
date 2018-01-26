using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SodgeIt.Workshop.DataAccessLayer;
using SodgeIt.Workshop.DomainModel;
using System.Linq.Expressions;

namespace SodgeIt.Workshop.Repository
{
    public interface IGenericRepository<T> where T : EntityBase
    {


        IEnumerable<T> Read(Expression<Func<T, bool>> predicate);

        IEnumerable<T> Read(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);

        IEnumerable<U> FilterRead<U>(Expression<Func<U, bool>> predicate) where U: T;

        IEnumerable<U> FilterRead<U>(Expression<Func<U, bool>> predicate, params Expression<Func<U, object>>[] paths) where U : T;

        bool InsertOrUpdate(T model);

        bool Delete(T model);

    }
}
