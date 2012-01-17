package IOCDomain;

public interface IStockQuote
{
    ILogger getLogger();
    IErrorHandler getErrorHandler();
    IDatabase getDatabase();
}

