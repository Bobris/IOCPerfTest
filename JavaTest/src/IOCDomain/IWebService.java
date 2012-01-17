package IOCDomain;

public interface IWebService
{
    IAuthenticator getAuthenticator();
    IStockQuote getStockQuote();
    void Execute();
}

