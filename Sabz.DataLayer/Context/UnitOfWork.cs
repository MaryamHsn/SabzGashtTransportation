//using Sabz.DataLayer.Repository;
//using Sabz.DomainClasses.DTO;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Sabz.DataLayer.Context
//{
//    public class UnitOfWork :IUnitOfWork
//    {
//        private ApplicationDbContext _dbContext;
//        public UnitOfWork(ApplicationDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }
//        #region defineRepository
//        private BaseRepository<DriverTbl, int> _drivers;
//        public IRepository<DriverTbl,int> Drivers
//        {
//            get
//            {
//                return _drivers ??
//                    (_drivers = new BaseRepository<DriverTbl, int>(_dbContext));
//            }
//        }
//        //public IDriverRepository driverRepository { get; set; }
//        #endregion
//        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
//        {
//            return _dbContext.Set<TEntity>();
//        }

//        public int SaveAllChanges()
//        {
//            return _dbContext.SaveChanges();
//        }

//        public IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
//        {
//            return ((DbSet<TEntity>)this.Set<TEntity>()).AddRange(entities);
//        }

//        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
//        {
//            _dbContext.Entry(entity).State = EntityState.Modified;
//        }

//        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
//        {
//            return _dbContext.Database.SqlQuery<T>(sql, parameters).ToList();
//        }

//        public void ForceDatabaseInitialize()
//        {
//            _dbContext.Database.Initialize(force: true);
//        }

//        public void Dispose()
//        {
//            _dbContext.Dispose();
//        }

//        //public  async Task<TEntity> GetAsync<TEntity,TU>(TU id, CancellationToken ct = new CancellationToken()) where TEntity : class where TU : struct
//        //{
//        //    return await ((DbSet<TEntity>)this.Set<TEntity>()).FindAsync(id, ct);
//        //}

//    }
//}
