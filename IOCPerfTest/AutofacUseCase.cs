using Autofac;
using IOCPerfTest.IOCDomain;

namespace IOCPerfTest
{
    public class AutofacUseCase : UseCase
    {
        readonly IContainer _container;

        public AutofacUseCase()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();
            builder.RegisterType<ErrorHandler>().As<IErrorHandler>();
            builder.RegisterType<Database>().As<IDatabase>();
            builder.RegisterType<Authenticator>().As<IAuthenticator>();
            builder.RegisterType<StockQuote>().As<IStockQuote>();
            builder.RegisterType<WebService>().As<IWebService>();
            _container = builder.Build();
        }

        public override IWebService Run()
        {
            var obj = _container.Resolve<IWebService>();
            obj.Execute();
            return obj;
        }
    }
}