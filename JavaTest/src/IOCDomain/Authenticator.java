package IOCDomain;

import com.google.inject.Inject;

public class Authenticator implements IAuthenticator {
    ILogger _logger;
    IErrorHandler _handler;
    IDatabase _database;

    @Inject
    public Authenticator(ILogger logger, IErrorHandler handler, IDatabase database) {
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

