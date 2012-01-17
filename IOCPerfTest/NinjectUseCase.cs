using IOCPerfTest.IOCDomain;
using Ninject;

namespace IOCPerfTest
{
    public class NinjectUseCase : UseCase
    {
        readonly IKernel _container;

        public NinjectUseCase()
        {
            var builder = new StandardKernel();
            builder.Bind<ILogger>().To<Logger>().InSingletonScope();
            builder.Bind<IErrorHandler>().To<ErrorHandler>();
            builder.Bind<IDatabase>().To<Database>();
            builder.Bind<IAuthenticator>().To<Authenticator>();
            builder.Bind<IStockQuote>().To<StockQuote>();
            builder.Bind<IWebService>().To<WebService>();
            _container = builder;
        }

        public override IWebService Run()
        {
            var obj = _container.Get<IWebService>();
            obj.Execute();
            return obj;
        }
    }
}