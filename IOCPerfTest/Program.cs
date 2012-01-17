using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using IOCPerfTest.IOCDomain;

namespace IOCPerfTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var useCaseTypes =
                typeof(Program).Assembly.GetTypes().Where(t => typeof(UseCase).IsAssignableFrom(t) && t != typeof(UseCase)).ToArray();
            useCaseTypes = useCaseTypes.Concat(useCaseTypes).ToArray();
            foreach (var useCaseType in useCaseTypes.OrderByDescending(t=>t.Name))
            {
                var sw = new Stopwatch();
                GC.Collect(GC.MaxGeneration);
                GC.WaitForPendingFinalizers();
                GC.Collect(GC.MaxGeneration);
                Logger.InstanceCount = 0;
                ErrorHandler.InstanceCount = 0;
                sw.Start();
                var uc = (UseCase)useCaseType.GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
                uc.Run();
                sw.Stop();
                var initTicks = sw.Elapsed.TotalMilliseconds;
                sw.Restart();
                for (int i = 0; i < 100000; i++) uc.AddToCheck(uc.Run());
                sw.Stop();
                var runTicks = sw.Elapsed.TotalMilliseconds;
                uc.Check();
                sw.Restart();
                uc = (UseCase) useCaseType.GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
                uc.Run();
                sw.Stop();
                var init2Ticks = sw.Elapsed.TotalMilliseconds;
                Console.WriteLine("{0,-20} {1,8:N1}ms init:{2,7:N2}ms 2nd init:{3,7:N2}ms ch: {4} {5}", useCaseType.Name, runTicks, initTicks, init2Ticks, Logger.InstanceCount, ErrorHandler.InstanceCount);
            }
        }
    }
}
