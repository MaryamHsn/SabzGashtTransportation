using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using StructureMap.Web;
using StructureMap;
using System.Data.Entity;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System;
using Sabz.DataLayer.Context;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.IService;
using Sabz.ServiceLayer;
using Sabz.ServiceLayer.Service;
using Sabz.DataLayer.IRepository;
using Sabz.DataLayer.Repository;

namespace Sabz.IocConfig
{
    public static class SmObjectFactory
    {
        private static readonly Lazy<Container> _containerBuilder =
            new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return _containerBuilder.Value; }
        }

        private static Container defaultContainer()
        {
            return new Container(ioc =>
            {

                ioc.For<DbContext>().Use(ctx => new ApplicationDbContext());
                //ioc.For<IUnitOfWork>()
                // .HybridHttpOrThreadLocalScoped()
                // .Use<UnitOfWork>();

                //ioc.For<IRepository>().Use<BaseRepository>().WithTheSameGenericType();
                ioc.Scan(y =>
                {
                    y.TheCallingAssembly();
                    y.AddAllTypesOf(typeof(IRepository<,>));
                    y.WithDefaultConventions();
                });
                ioc.For(typeof(IRepository<,>)).DecorateAllWith(typeof(BaseRepository<,>));
                ioc.For(typeof(IRepository<,>)).Use(typeof(BaseRepository<,>));
                ioc.Scan(x =>
                {
                    x.AssemblyContainingType(typeof(DriverRepository));
                    x.AddAllTypesOf(typeof(IRepository<,>));
                    x.ConnectImplementationsToTypesClosing(typeof(IRepository<,>));
                });
                ioc.For<DbContext>().Use(ctx => new ApplicationDbContext());
                ioc.For(typeof(IRepository<,>)).Use(typeof(BaseRepository<,>));
                ioc.For(typeof(IRepository<,>)).DecorateAllWith(typeof(BaseRepository<,>));
                ioc.Scan(
            scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
                scan.AssemblyContainingType<ApplicationDbContext>();
                scan.ConnectImplementationsToTypesClosing(typeof(IRepository<,>));


            });
                //ioc.For( typeof(BaseRepository<,>) )
                //   .HybridHttpOrThreadLocalScoped()
                //   .Use(typeof(IRepository<,>));
                ioc.For(typeof(IRepository<,>))
             .HybridHttpOrThreadLocalScoped()
             .Use(typeof(BaseRepository<,>));


                ioc.For<Microsoft.AspNet.SignalR.IDependencyResolver>()
                   .Singleton()
                   .Add<StructureMapSignalRDependencyResolver>();

                ioc.For<IIdentity>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use(() => getIdentity());

                ioc.For<IUnitOfWork>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use<ApplicationDbContext>();
                // Remove these 2 lines if you want to use a connection string named connectionString1, defined in the web.config file.
                //.Ctor<string>("connectionString")
                //.Is("Data Source=(local);Initial Catalog=TestDbIdentity;Integrated Security = true");

                ioc.For<ApplicationDbContext>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use(context => (ApplicationDbContext)context.GetInstance<IUnitOfWork>());
                ioc.For<DbContext>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use(context => (ApplicationDbContext)context.GetInstance<IUnitOfWork>());


                ioc.For<IUserStore<ApplicationUser, int>>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<CustomUserStore>();

                ioc.For<IRoleStore<CustomRole, int>>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<CustomRoleStore>();

                ioc.For<IAuthenticationManager>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use(() => HttpContext.Current.GetOwinContext().Authentication);

                ioc.For<IApplicationSignInManager>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<ApplicationSignInManager>();

                ioc.For<IApplicationRoleManager>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<ApplicationRoleManager>();

                // map same interface to different concrete classes
                ioc.For<IIdentityMessageService>().Use<SmsService>();
                ioc.For<IIdentityMessageService>().Use<EmailService>();

                ioc.For<IApplicationUserManager>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use<ApplicationUserManager>()
                   .Ctor<IIdentityMessageService>("smsService").Is<SmsService>()
                   .Ctor<IIdentityMessageService>("emailService").Is<EmailService>()
                   .Setter(userManager => userManager.SmsService).Is<SmsService>()
                   .Setter(userManager => userManager.EmailService).Is<EmailService>();

                ioc.For<ApplicationUserManager>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use(context => (ApplicationUserManager)context.GetInstance<IApplicationUserManager>());

                ioc.For<ICustomRoleStore>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<CustomRoleStore>();

                ioc.For<ICustomUserStore>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<CustomUserStore>();

                //config.For<IDataProtectionProvider>().Use(() => app.GetDataProtectionProvider()); // In Startup class
                //Repository
                          ioc.For<IAccidentRepository>()
                              .HybridHttpOrThreadLocalScoped()
                              .Use<AccidentRepository>();

                ioc.For<IAutomobileRepository>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<AutomobileRepository>();

                ioc.For<IAutomobileTypeRepository>()
          .HybridHttpOrThreadLocalScoped()
          .Use<AutomobileTypeRepository>();


                ioc.For<IDriverRepository>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<DriverRepository>();

                ioc.For<IDriverRoutRepository>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<DriverRoutRepository>();

                ioc.For<ILogDriverRoutRepository>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<LogDriverRoutRepository>();

                ioc.For<IPaymentRepository>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<PaymentRepository>();

                ioc.For<IRegionRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<RegionRepository>();

                ioc.For<IRepairmentRepository>()
             .HybridHttpOrThreadLocalScoped()
             .Use<RepairmentRepository>();

                ioc.For<IRoutRepository>()
      .HybridHttpOrThreadLocalScoped()
      .Use<RoutRepository>();
                ioc.For(typeof(IAccidentRepository)).Use(typeof(AccidentRepository));
                ioc.For(typeof(IAutomobileRepository)).Use(typeof(AutomobileRepository));
                ioc.For(typeof(IAutomobileTypeRepository)).Use(typeof(AutomobileTypeRepository));
                ioc.For(typeof(IDriverRepository)).Use(typeof(DriverRepository));
                ioc.For(typeof(ILogDriverRoutRepository)).Use(typeof(LogDriverRoutRepository));
                ioc.For(typeof(IPaymentRepository)).Use(typeof(PaymentRepository));
                ioc.For(typeof(IRegionRepository)).Use(typeof(RegionRepository));
                ioc.For(typeof(IRepairmentRepository)).Use(typeof(RepairmentRepository));
                ioc.For(typeof(IRoutRepository)).Use(typeof(RoutRepository));

                /////
                ioc.For<IAccidentService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<EfAccidentService>();

                ioc.For<IAutomobileService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<EfAutomobileService>();

                ioc.For<IAutomobileTypeService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<EfAutomobileTypeService>();

                ioc.For<IDriverRoutService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<EfDriverRoutService>();

                ioc.For<IDriverService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<EfDriverService>();

                ioc.For<ILogDriverRoutService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<EfLogDriverRoutService>();

                ioc.For<IRegionService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<EfRegionService>();

                ioc.For<IRepairmentService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<EfRepairmentService>();

                ioc.For<IRoutService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<EfRoutService>();

                ioc.For<IPaymentService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<EfPaymentService>();
            });
        }

        private static IIdentity getIdentity()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                return HttpContext.Current.User.Identity;
            }

            return ClaimsPrincipal.Current != null ? ClaimsPrincipal.Current.Identity : null;
        }
    }
}