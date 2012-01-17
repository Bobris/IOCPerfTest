import IOCDomain.*;

public class PlainUseCase extends UseCase {
    ILogger _logger;

    public PlainUseCase() {
        _logger = new Logger();
    }

    @Override
    public IWebService Run() {
        IWebService service = new WebService(
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
        service.Execute();
        return service;
    }
}

