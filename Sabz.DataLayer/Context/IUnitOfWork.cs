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
       IRepository<AccidentTbl,int> AccidentRepository { get; }
       IRepository<AutomobileTbl,int> AutomobileRepository { get; }
       IRepository<AutomobileTypeTbl,int> AutomobileTypeRepository { get; }
       IRepository<DriverRoutTbl,int> DriverRoutRepository { get; }
       IRepository<DriverTbl,int> DriverRepository { get; }
       IRepository<LogDriverRoutTbl,int> LogDriverRoutRepository { get; }
       IRepository<PaymentTbl,int> PaymentRepository { get; }
       IRepository<RegionTbl,int> RegionRepository { get; }
       IRepository<RepairmentTbl,int> RepairmentRepository { get; }
       IRepository<RoutTbl,int> RoutRepository { get; }
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveAllChanges();
        void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;
        IList<T> GetRows<T>(string sql, params object[] parameters) where T : class;
        IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void ForceDatabaseInitialize();
       // Task<TEntity> GetAsync<TEntity, TU>(TU id, CancellationToken ct = new CancellationToken()) where TEntity : class where TU : struct;


  }
}