using StructureMap;
using IOCPerfTest.IOCDomain;

namespace IOCPerfTest
{
    public class StructureMapUseCase : UseCase
    {
        readonly IContainer _container;

        public StructureMapUseCase()
        {
            _container = new Container(ce=>
                {
                    ce.For<ILogger>().Singleton().Use<Logger>();
                    ce.For<IErrorHandler>().Transient().AlwaysUnique().Use<ErrorHandler>();
                    ce.For<IDatabase>().Transient().AlwaysUnique().Use<Database>();
                    ce.For<IAuthenticator>().Transient().AlwaysUnique().Use<Authenticator>();
                    ce.For<IStockQuote>().Transient().AlwaysUnique().Use<StockQuote>();
                    ce.For<IWebService>().Transient().AlwaysUnique().Use<WebService>();
                });
        }

        public override IWebService Run()
        {
            var obj = _container.GetInstance<IWebService>();
            obj.Execute();
            return obj;
        }
    }
}