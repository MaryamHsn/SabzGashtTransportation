using Sabz.DataLayer.Context;
using Sabz.DomainClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sabz.DataLayer.Repository
{
    public  class BaseRepository<T, TU> :IRepository<T, TU>
       where T : BaseEntity<TU>
       where TU : struct
    {
        protected readonly ApplicationDbContext _context;
        //IUnitOfWork _uow;
        protected readonly IDbSet<T> _dbSet;
        protected readonly DateTime _now;

        public BaseRepository(ApplicationDbContext contextFactory)
        {
            _context = contextFactory;
            _dbSet = _context.Set<T>();
            _now = DateTime.Now;
        }
        public virtual T Add(T entity)
        {
            entity.CreatedDate = _now;
            entity.ModifiedDate = _now;
            entity.IsActive = true;

            _dbSet.Add(entity);

            return entity;
        }

        public virtual T Update(T entity)
        {
            var old = Get(entity.Id);
            _context.Entry(old).State = EntityState.Detached;

            entity.ModifiedDate = _now;
            entity.CreatedDate = old.CreatedDate;

            _context.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public virtual bool Delete(T entity)
        {
            _dbSet.Remove(entity);

            return true;
        }

        public virtual bool Delete(Expression<Func<T, bool>> where)
        {
            var entities = _dbSet.Where(where).AsEnumerable();
            foreach (var entity in entities)
                _dbSet.Remove(entity);

            return true;
        }

        public bool SoftDelete(T entity)
        {
            entity.IsActive = false;
            Update(entity);

            return true;
        }

        public bool SoftDelete(Expression<Func<T, bool>> where)
        {
            var entities = _dbSet.Where(where).AsEnumerable();
            foreach (var entity in entities)
            {
                entity.ModifiedDate = _now;
                entity.IsActive = false;
                _context.Entry(entity).State = EntityState.Modified;
            }

            return true;
        }

        public virtual T Get(TU id)
        {
            return _dbSet.Find(id);
        }

        public virtual ICollection<T> Get(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }

        public virtual ICollection<T> Get()
        {
            return _dbSet.ToList();
        }

        //public virtual IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where,
        //    Expression<Func<T, TOrder>> order)
        //{
        //    var results = _dbSet.OrderBy(order).Where(where).GetPage(page).ToList();
        //    var total = _dbSet.Count(where);
        //    return new StaticPagedList<T>(results, page.PageNumber, page.PageSize, total);
        //}
        //     public virtual IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where,
        //    Expression<Func<T, TOrder>> order)
        //{
        //    var results = _dbSet.OrderBy(order).Where(where).GetPage(page).ToList();
        //    var total = _dbSet.Count(where);
        //    return new StaticPagedList<T>(results, page.PageNumber, page.PageSize, total);
        //}

        public virtual async Task<T> AddAsync(T entity, CancellationToken ct = new CancellationToken())
        {
            entity.ModifiedDate = _now;
            entity.CreatedDate = _now;
            entity.IsActive = true;

            _dbSet.Add(entity);

            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity, CancellationToken ct = new CancellationToken())
        {
            var old = await GetAsync(entity.Id, ct);
            _context.Entry(old).State = EntityState.Detached;

            entity.ModifiedDate = _now;
            entity.CreatedDate = old.CreatedDate;
            _context.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public virtual async Task<bool> DeleteAsync(T entity, CancellationToken ct = new CancellationToken())
        {
            _dbSet.Remove(entity);

            return true;
        }

        public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> where,
            CancellationToken ct = new CancellationToken())
        {
            var entities = _dbSet.Where(where).AsEnumerable();
            foreach (var entity in entities)
                _dbSet.Remove(entity);

            return true;
        }

        public async Task<bool> SoftDeleteAsync(T entity, CancellationToken ct = new CancellationToken())
        {
            entity.IsActive = false;
            await UpdateAsync(entity, ct);

            return true;
        }

        public async Task<bool> SoftDeleteAsync(Expression<Func<T, bool>> @where,
            CancellationToken ct = new CancellationToken())
        {
            var entities = _dbSet.Where(where).AsEnumerable();
            foreach (var entity in entities)
            {
                entity.IsActive = false;
                entity.ModifiedDate = _now;

                _context.Entry(entity).State = EntityState.Modified;
            }

            return true;
        }

        public virtual async Task<T> GetAsync(TU id, CancellationToken ct = new CancellationToken())
        {
            return _context.Set<T>().Find(ct, id);

        }

        public virtual async Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> @where,
            CancellationToken ct = new CancellationToken())
        {
            return await _dbSet.Where(where).ToListAsync(ct);
        }

        public virtual async Task<ICollection<T>> GetAsync(CancellationToken ct = new CancellationToken())
        {
            return await _dbSet.ToListAsync(ct);
        }

        //public virtual async Task<IPagedList<T>> GetPageAsync<TOrder>(Page page, Expression<Func<T, bool>> where,
        //    Expression<Func<T, TOrder>> order, CancellationToken ct = new CancellationToken())
        //{
        //    var results = await _dbSet.OrderBy(order).Where(where).GetPage(page).ToListAsync(ct);
        //    var total = await _dbSet.CountAsync(where, ct);

        //    return new StaticPagedList<T>(results, page.PageNumber, page.PageSize, total);
        //}

        public virtual int Count(Expression<Func<T, bool>> @where)
        {
            return _dbSet.Count(where);
        }

        public virtual int Count()
        {
            return _dbSet.Count();
        }

        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Count(predicate) > 0;
        }

        public virtual bool Any(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }

        public virtual async Task<ICollection<T>> GetLimitAsync(Expression<Func<T, bool>> @where,
            int take, int skip, CancellationToken ct = new CancellationToken())
        {
            return await _dbSet.Where(where).OrderByDescending(x => x.Id).Skip(skip).Take(take).ToListAsync(ct);
        }
    }
}