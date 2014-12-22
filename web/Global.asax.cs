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
using web.core.Cache;
using web.core.Repositories;
using System.Data.Entity;
using web.core.Mappers;
using dal;
using web.core.Authentication;

namespace web
{
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

        public override void Init()
        {
            this.PostAuthenticateRequest += this.PostAuthenticateRequestHandler;
            base.Init();
        }

        void PostAuthenticateRequestHandler(object sender, EventArgs e)
        {
            HttpCookie authCookie = this.Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (IsValidAuthCookie(authCookie))
            {
                var formsAuthentication = DependencyResolver.Current.GetService<IFormsAuthentication>();

                var ticket = formsAuthentication.Decrypt(authCookie.Value);
                var efmvcUser = new UserModel(ticket);
                string[] userRoles = efmvcUser.RoleName.Split(',');
                this.Context.User = new GenericPrincipal(efmvcUser, userRoles);
                formsAuthentication.SetAuthCookie(this.Context, ticket);
            }
        }
        private static bool IsValidAuthCookie(HttpCookie authCookie)
        {
            return authCookie != null && !String.IsNullOrEmpty(authCookie.Value);
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
            _kernel.Bind<DbContext>().To<NorthwindContext>().InThreadScope();
            _kernel.Bind(typeof(IRepository<>)).To(typeof(RepositoryBase<>));
            _kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            _kernel.Bind<IFormsAuthentication>().To<DefaultFormsAuthentication>();

            _kernel.Bind<IProductRepository>().To<ProductRepository>();
            //_kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            _kernel.Bind<ISupplierRepository>().To<SupplierRepository>();

            _kernel.Bind<ICacheProvider>().To<MemoryCacheProvider>();
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