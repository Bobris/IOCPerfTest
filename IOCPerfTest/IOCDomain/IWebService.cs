namespace IOCPerfTest.IOCDomain
{
	public interface IWebService
	{
		IAuthenticator Authenticator { get; }
		IStockQuote StockQuote { get; }
		void Execute();
	}
}