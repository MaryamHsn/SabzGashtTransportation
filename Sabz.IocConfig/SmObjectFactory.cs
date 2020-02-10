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

                //config.For<IDataProtectionProvider>().Use(()=> app.GetDataProtectionProvider()); // In Startup class

                ioc.For<ICategoryService>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use<EfCategoryService>();

                ioc.For<IProductService>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use<EfProductService>();

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

                ioc.For<ILogRoutDriverService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<EfLogRoutDriverService>();

                ioc.For<IRegionService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<EfRegionService>();

                ioc.For<IRepairmentService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<EfRepairmentService>();

                ioc.For<IRoutService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<EfRoutService>();
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