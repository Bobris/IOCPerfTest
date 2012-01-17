package IOCDomain;

public interface IDatabase
{
    ILogger getLogger();
    IErrorHandler getErrorHandler();
}

