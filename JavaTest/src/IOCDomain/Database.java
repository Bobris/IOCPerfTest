package IOCDomain;

import com.google.inject.Inject;

public class Database implements IDatabase {
    ILogger _logger;
    IErrorHandler _handler;

    @Inject
    public Database(IErrorHandler handler, ILogger logger) {
        _handler = handler;
        _logger = logger;
    }

    @Override
    public ILogger getLogger() {
        return _logger;
    }

    @Override
    public IErrorHandler getErrorHandler() {
        return _handler;
    }
}

