using Sabz.DataLayer.Repository;
using Sabz.DomainClasses.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Sabz.DataLayer.Context
{
    public interface IUnitOfWork : IDisposable
    {
       IRepository<DriverTbl,int> Driver { get; }
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveAllChanges();
        void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;
        IList<T> GetRows<T>(string sql, params object[] parameters) where T : class;
        IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void ForceDatabaseInitialize();
       // Task<TEntity> GetAsync<TEntity, TU>(TU id, CancellationToken ct = new CancellationToken()) where TEntity : class where TU : struct;


  }
}