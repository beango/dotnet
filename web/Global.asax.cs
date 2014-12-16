using dal.ef.core;
using dal.ef.Repositories;
using model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using web.Mappers;

namespace web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyResolver.SetResolver(new NinjectDependencyResolver());
            AutoMapperConfiguration.Configure();
        }

        public MvcApplication()
        {
            AuthorizeRequest += new EventHandler(MvcApplication_AuthorizeRequest);
        }

        void MvcApplication_AuthorizeRequest(object sender, EventArgs e)
        {
            var id = Context.User.Identity as FormsIdentity;
            if (id != null && id.IsAuthenticated)
            {
                var roles = id.Ticket.UserData.Split(',');
                Context.User = new GenericPrincipal(id, roles);
            }
        }
    }

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            _kernel.Settings.InjectNonPublic = true;
            AddBindings();
        }

        private void AddBindings()
        {
            _kernel.Bind<IDatabaseFactory>().To<DatabaseFactory>().InThreadScope();
            _kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            _kernel.Bind<IProductRepository>().To<ProductRepository>();

            //_kernel.Bind(typeof(IDao<>)).To(typeof(DaoTemplate<>));
            //_kernel.Bind<ICacheProvider>().To<MemoryCacheProvider>();
            //_kernel.Bind<IProductRepository>().To<IRepository>();
            //_kernel.Components.Add<IPlanningStrategy, CachePlanningStrategy<InterceptCacheAttribute, CacheInterceptor>>();
            //_kernel.Bind<SYS_USER_DAL>().ToSelf();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}