import IOCDomain.*;

import java.util.ArrayList;

public abstract class UseCase
{
    ArrayList<IWebService> _array;

    public UseCase()
    {
        _array = new ArrayList<IWebService>();
    }

    public abstract IWebService Run();

    public void AddToCheck(IWebService check)
    {
        _array.add(check);
    }

    public void Check() throws Exception
    {
        for(int i = 1; i<_array.size();i++)
        {
            if (_array.get(i).getAuthenticator().getDatabase().getErrorHandler() == _array.get(i - 1).getAuthenticator().getDatabase().getErrorHandler()) throw new Exception("bad");
            if (_array.get(i).getStockQuote().getDatabase().getErrorHandler() == _array.get(i - 1).getStockQuote().getDatabase().getErrorHandler()) throw new Exception("bad");
            if (_array.get(i).getStockQuote().getDatabase().getErrorHandler().getLogger() != _array.get(i - 1).getStockQuote().getDatabase().getErrorHandler().getLogger()) throw new Exception("bad");
        }
    }
}

