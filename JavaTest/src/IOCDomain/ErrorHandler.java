package IOCDomain;

import com.google.inject.Inject;

public class ErrorHandler implements IErrorHandler {
    public static int InstanceCount;
    ILogger _logger;

    @Inject
    public ErrorHandler(ILogger logger) {
        _logger = logger;
        InstanceCount++;
    }

    @Override
    public ILogger getLogger() {
        return _logger;
    }
}

