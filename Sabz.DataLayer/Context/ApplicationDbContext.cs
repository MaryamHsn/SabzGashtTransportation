using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using Sabz.DomainClasses.DTO;

namespace Sabz.DataLayer.Context
{
    public class ApplicationDbContext :
        IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>,
        IUnitOfWork
    {
        /// <summary>
        /// It looks for a connection string named connectionString1 in the web.config file.
        /// </summary>
        public ApplicationDbContext()
            : base("connectionString1")
        {
            //this.Database.Log = data => System.Diagnostics.Debug.WriteLine(data);
        }
        public DbSet<AccidentTbl> Accidents { set; get; }
        public DbSet<AutomobileTbl> Automobiles { set; get; }
        public DbSet<AutomobileTypeTbl> AutomobileTypes { set; get; }
        public DbSet<DriverRoutTbl> Rout_Drivers { set; get; }
        public DbSet<DriverTbl> Drivers { set; get; }
        public DbSet<LogRoutDriverTbl> LogRoutDriver { set; get; }
        public DbSet<RegionTbl> Regions { set; get; }
        public DbSet<RepairmentTbl> Repairments { set; get; }
        public DbSet<RoutTbl> Routs { set; get; }
        public DbSet<PaymentTbl> Payments { set; get; }


        /// <summary>
        /// To change the connection string at runtime. See the SmObjectFactory class for more info.
        /// </summary>
        //public ApplicationDbContext(string connectionString)
        //    : base(connectionString)
        //{
        //    //Note: defaultConnectionFactory in the web.config file should be set.
        //}

        //protected override void OnModelCreating(DbModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<ApplicationUser>().ToTable("Users");
        //    builder.Entity<CustomRole>().ToTable("Roles");
        //    builder.Entity<CustomUserClaim>().ToTable("UserClaims");
        //    builder.Entity<CustomUserRole>().ToTable("UserRoles");
        //    builder.Entity<CustomUserLogin>().ToTable("UserLogins");

        //    builder.Entity<AccidentTbl>().ToTable("Accident");
        //    builder.Entity<AutomobileTbl>().ToTable("Automobile");
        //    builder.Entity<AutomobileTypeTbl>().ToTable("AutomobileType");
        //    builder.Entity<DriverRoutTbl>().ToTable("DriverRout");
        //    builder.Entity<DriverTbl>().ToTable("Driver");
        //    builder.Entity<LogRoutDriverTbl>().ToTable("LogRoutDriver");
        //    builder.Entity<RegionTbl>().ToTable("Region");
        //    builder.Entity<RepairmentTbl>().ToTable("Repairment");
        //    builder.Entity<RoutTbl>().ToTable("Rout");

        //    builder.Entity<AccidentTbl>()
        //    .Property(e => e.Cost)
        //    .HasPrecision(18, 0);

        //    builder.Entity<AutomobileTbl>()
        //        .Property(e => e.Shasi)
        //        .IsFixedLength();

        //    builder.Entity<AutomobileTbl>()
        //        .Property(e => e.CreateYear)
        //        .IsFixedLength();

        //    builder.Entity<AutomobileTbl>()
        //        .HasMany(e => e.DriverTbls)
        //        .WithRequired(e => e.AutomobileTbl)
        //        .HasForeignKey(e => e.AutomobileId)
        //        .WillCascadeOnDelete(false);

        //    builder.Entity<AutomobileTypeTbl>()
        //        .HasMany(e => e.AutomobileTbls)
        //        .WithOptional(e => e.AutomobileTypeTbl)
        //        .HasForeignKey(e => e.AutomobileTypeId);

        //    builder.Entity<AutomobileTypeTbl>()
        //        .HasMany(e => e.RoutTbls)
        //        .WithRequired(e => e.AutomobileTypeTbl)
        //        .HasForeignKey(e => e.AutomobileTypeId)
        //        .WillCascadeOnDelete(false);

        //    builder.Entity<DriverRoutTbl>()
        //        .HasMany(e => e.LogRoutDriverTbls)
        //        .WithRequired(e => e.DriverRoutTbl)
        //        .HasForeignKey(e => e.DriverRoutId)
        //        .WillCascadeOnDelete(false);

        //    builder.Entity<DriverTbl>()
        //        .Property(e => e.NationalCode)
        //        .IsFixedLength();

        //    builder.Entity<DriverTbl>()
        //        .HasMany(e => e.DriverRoutTbls)
        //        .WithRequired(e => e.DriverTbl)
        //        .WillCascadeOnDelete(false);

        //    builder.Entity<LogRoutDriverTbl>()
        //        .Property(e => e.FinePrice)
        //        .HasPrecision(18, 0);

        //    builder.Entity<RegionTbl>()
        //        .HasMany(e => e.RoutTbls)
        //        .WithRequired(e => e.RegionTbl)
        //        .WillCascadeOnDelete(false);

        //    builder.Entity<RepairmentTbl>()
        //        .Property(e => e.Cost)
        //        .HasPrecision(18, 0);

        //    builder.Entity<RoutTbl>()
        //        .Property(e => e.AgreementPrice)
        //        .HasPrecision(18, 0);

        //    builder.Entity<RoutTbl>()
        //        .Property(e => e.DriverPrice)
        //        .HasPrecision(18, 0);

        //    builder.Entity<RoutTbl>()
        //        .HasMany(e => e.DriverRoutTbls)
        //        .WithRequired(e => e.RoutTbl)
        //        .WillCascadeOnDelete(false);
        //}

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public int SaveAllChanges()
        {
            return base.SaveChanges();
        }

        public IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            return ((DbSet<TEntity>)this.Set<TEntity>()).AddRange(entities);
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public void ForceDatabaseInitialize()
        {
            this.Database.Initialize(force: true);
        }
    }
}