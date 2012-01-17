using Munq;
using IOCPerfTest.IOCDomain;

namespace IOCPerfTest
{
    public class MunqUseCase : UseCase
    {
        readonly IocContainer _container;

        public MunqUseCase()
        {
            _container = new IocContainer();
            _container.Register<ILogger,Logger>().AsContainerSingleton();
            _container.Register<IErrorHandler,ErrorHandler>();
            _container.Register<IDatabase,Database>();
            _container.Register<IAuthenticator,Authenticator>();
            _container.Register<IStockQuote,StockQuote>();
            _container.Register<IWebService,WebService>();
        }

        public override IWebService Run()
        {
            var obj = _container.Resolve<IWebService>();
            obj.Execute();
            return obj;
        }
    }
}