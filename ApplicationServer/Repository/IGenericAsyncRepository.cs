using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using JoergIsAGeek.Workshop.DataAccessLayer;
using JoergIsAGeek.Workshop.DomainModel;
using System.Linq.Expressions;
using JoergIsAGeek.Workshop.DomainModel.Abstracts;
using JoergIsAGeek.Workshop.DataAccessLayer.ControlModels;
using System.Threading.Tasks;

namespace JoergIsAGeek.Workshop.Repository
{
    public interface IGenericAsyncRepository<T> where T : EntityBase
    {

        Task<T> Find(int id);

        Task<IEnumerable<T>> Read(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> Read(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);

        Task<IEnumerable<U>> FilterRead<U>(Expression<Func<U, bool>> predicate) where U: T;

        Task<IEnumerable<U>> FilterRead<U>(Expression<Func<U, bool>> predicate, params Expression<Func<U, object>>[] paths) where U : T;

        Task<int> Count(Expression<Func<T, bool>> predicate);

        Task<ErrorModel> InsertOrUpdate(T model, string userName);

        Task<ErrorModel> Delete(T model);

    }
}
