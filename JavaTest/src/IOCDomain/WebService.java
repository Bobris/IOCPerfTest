package IOCDomain;

import com.google.inject.Inject;

public class WebService implements IWebService {
    IAuthenticator _authenticator;
    IStockQuote _quotes;

    @Inject
    public WebService(IAuthenticator authenticator, IStockQuote quotes) {
        _authenticator = authenticator;
        _quotes = quotes;
    }

    @Override
    public IAuthenticator getAuthenticator() {
        return _authenticator;
    }

    @Override
    public IStockQuote getStockQuote() {
        return _quotes;
    }

    public void Execute() {
    }
}
