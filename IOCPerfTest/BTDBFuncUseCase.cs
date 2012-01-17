using System;
using BTDB.IOC;
using IOCPerfTest.IOCDomain;

namespace IOCPerfTest
{
    public class BTDBFuncUseCase : UseCase
    {
        readonly IContainer _container;
        readonly Func<IWebService> _factory;

        public BTDBFuncUseCase()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();
            builder.RegisterAssemblyTypes(typeof(IWebService).Assembly)
                .Where(t => t.Namespace == "IOCPerfTest.IOCDomain")
                .AsImplementedInterfaces()
                .PreserveExistingDefaults();
            _container = builder.Build();
            _factory = _container.Resolve<Func<IWebService>>();
        }

        public override IWebService Run()
        {
            var obj = _factory();
            obj.Execute();
            return obj;
        }
    }
}