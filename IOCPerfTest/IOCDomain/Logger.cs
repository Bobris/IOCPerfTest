namespace IOCPerfTest.IOCDomain
{
	public class Logger : ILogger
	{
        public static int InstanceCount;

	    public Logger()
	    {
            InstanceCount++;
	    }

		public bool Verbose { get; set; }
	}
}
