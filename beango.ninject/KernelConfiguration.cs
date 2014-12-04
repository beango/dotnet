using Ninject;
using Ninject.Extensions.Interception.Infrastructure.Language;

namespace beango.ninject
{
    class KernelConfiguration
    {
        private static IKernel _kernel;

        public static IKernel Kernel
        {
            get
            {
                if (_kernel == null)
                {
                    var kernel = new StandardKernel();

                    kernel.Bind<IService>().To<Service>().Intercept().With<LoggingInterceptor>();
                    _kernel = kernel;
                }

                return _kernel;
            }
        }
    }
}
