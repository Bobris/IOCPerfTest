using IOCPerfTest.IOCDomain;
using Dynamo.Ioc;

namespace IOCPerfTest
{
    public class DynamoUseCase : UseCase
    {
        readonly Container _container;

        public DynamoUseCase()
        {
            _container = new Container();
            _container.Register<ILogger,Logger>().WithLifetime(new ContainerLifetime());
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