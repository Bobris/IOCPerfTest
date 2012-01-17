using System;
using System.Collections.Generic;
using IOCPerfTest.IOCDomain;

namespace IOCPerfTest
{
    public abstract class UseCase
    {
        readonly List<IWebService> _array = new List<IWebService>();
        public abstract IWebService Run();

        public void AddToCheck(IWebService check)
        {
            _array.Add(check);
        }

        public void Check()
        {
            for(int i=1;i<_array.Count;i++)
            {
                if (_array[i].Authenticator.Database.ErrorHandler == _array[i - 1].Authenticator.Database.ErrorHandler) throw new Exception("bad");
                if (_array[i].StockQuote.Database.ErrorHandler == _array[i - 1].StockQuote.Database.ErrorHandler) throw new Exception("bad");
                if (_array[i].StockQuote.Database.ErrorHandler.Logger != _array[i - 1].StockQuote.Database.ErrorHandler.Logger) throw new Exception("bad");
            }
        }
    }
}