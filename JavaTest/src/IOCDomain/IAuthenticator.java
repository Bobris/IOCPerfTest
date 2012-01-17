package IOCDomain;

public interface IAuthenticator {
    ILogger getLogger();
    IErrorHandler getErrorHandler();
    IDatabase getDatabase();
}

