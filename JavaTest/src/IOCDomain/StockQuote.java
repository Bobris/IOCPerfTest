package IOCDomain;

import com.google.inject.Inject;

public class StockQuote implements IStockQuote {
    private ILogger _logger;
    private IErrorHandler _handler;
    private IDatabase _database;

    @Inject
    public StockQuote(ILogger logger, IErrorHandler handler, IDatabase database) {
        _logger = logger;
        _handler = handler;
        _database = database;
    }

    @Override
    public ILogger getLogger() {
        return _logger;
    }

    @Override
    public IErrorHandler getErrorHandler() {
        return _handler;
    }

    @Override
    public IDatabase getDatabase() {
        return _database;
    }
}

