using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using IOCPerfTest.IOCDomain;

namespace IOCPerfTest
{
    public class WindsorUseCase : UseCase
    {
        readonly IWindsorContainer _container;

        public WindsorUseCase()
        {
            _container = new WindsorContainer();
            _container.Install(new IOCDomainInstaller());
        }

        class IOCDomainInstaller : IWindsorInstaller
        {
            public void Install(IWindsorContainer container, IConfigurationStore store)
            {
                container.Register(Component.For<ILogger>().ImplementedBy<Logger>().LifestyleSingleton());
                container.Register(
                    Classes.FromThisAssembly().InSameNamespaceAs<IWebService>().WithServiceDefaultInterfaces().
                        LifestyleTransient());
            }
        }

        public override IWebService Run()
        {
            var obj = _container.Resolve<IWebService>();
            obj.Execute();
            return obj;
        }
    }
}