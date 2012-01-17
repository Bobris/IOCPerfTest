using IOCPerfTest.IOCDomain;

namespace IOCPerfTest
{
    public class PlainUseCase : UseCase
    {
        readonly ILogger _logger;

        public PlainUseCase()
        {
            _logger = new Logger();
        }

        public override IWebService Run()
        {
            var obj = new WebService(
                new Authenticator(
                    _logger,
                    new ErrorHandler(_logger),
                    new Database(new ErrorHandler(_logger), _logger)
                    ),
                new StockQuote(
                    _logger,
                    new ErrorHandler(_logger),
                    new Database(new ErrorHandler(_logger), _logger)
                    )
                );
            obj.Execute();
            return obj;
        }
    }
}