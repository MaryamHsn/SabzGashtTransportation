using Sabz.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sabz.DataLayer.Repository
{
    public interface IRepository<T, in TU> where T : BaseEntity<TU> where TU : struct
    {
        T Add(T entity);
        T Update(T entity);
        bool Delete(T entity);
        bool Delete(Expression<Func<T, bool>> where);
        bool SoftDelete(T entity);
        bool SoftDelete(Expression<Func<T, bool>> where);
        T Get(TU id);
        T Get(Expression<Func<T, bool>> where);
        ICollection<T> GetAll(Expression<Func<T, bool>> where);
        ICollection<T> GetAll();
      //  IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order);

        Task<T> AddAsync(T entity, CancellationToken ct = default(CancellationToken));
        Task<T> UpdateAsync(T entity, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(T entity, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(Expression<Func<T, bool>> where, CancellationToken ct = default(CancellationToken));
        Task<bool> SoftDeleteAsync(T entity, CancellationToken ct = default(CancellationToken));
        Task<bool> SoftDeleteAsync(Expression<Func<T, bool>> where, CancellationToken ct = default(CancellationToken));
        Task<T> GetAsync(TU id, CancellationToken ct = default(CancellationToken));
        Task<T> GetAsync(Expression<Func<T, bool>> where, CancellationToken ct = new CancellationToken());
        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> where, CancellationToken ct = default(CancellationToken));
        Task<ICollection<T>> GetAllAsync(CancellationToken ct = default(CancellationToken));
      //  Task<IPagedList<T>> GetPageAsync<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order, CancellationToken ct = default(CancellationToken));
        int Count(Expression<Func<T, bool>> @where);
        int Count();
        bool Exists(Expression<Func<T, bool>> predicate);
        bool Any(Expression<Func<T, bool>> predicate);

        Task<ICollection<T>> GetLimitAsync(Expression<Func<T, bool>> @where, int take, int skip,
            CancellationToken ct = new CancellationToken());
    }
}
