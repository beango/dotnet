using System;
using System.Collections.Generic;
using Ninject;
using beango.dal;
using beango.model;
using beango.northwindmvc.Module;

namespace beango.northwindmvc.util
{
    public class NinjectDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new Ninject.StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            kernel.Bind(typeof(IDao<>)).To(typeof(DaoMongo<>));
            kernel.Bind<IUserState>().To<UserAuthModule>();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}