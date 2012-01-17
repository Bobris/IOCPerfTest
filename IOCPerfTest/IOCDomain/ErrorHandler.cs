namespace IOCPerfTest.IOCDomain
{
	public class ErrorHandler : IErrorHandler
	{
	    public static int InstanceCount;

	    readonly ILogger _logger;

		public ErrorHandler(ILogger logger)
		{
			_logger = logger;
		    InstanceCount++;
		}

		public ILogger Logger { get { return _logger; } }
	}
}
