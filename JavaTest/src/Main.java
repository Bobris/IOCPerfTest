import IOCDomain.ErrorHandler;
import IOCDomain.Logger;

import java.lang.ref.WeakReference;
import java.util.*;

public class Main {

    public static void gc() {
        Object obj = new Object();
        WeakReference ref = new WeakReference<Object>(obj);
        obj = null;
        while(ref.get() != null) {
            System.gc();
        }
    }
    public static void main(String[] args) throws Exception, InstantiationException {
        List<Class> useCaseClasses = new ArrayList<Class> ();
        useCaseClasses.add( PlainUseCase.class );
        useCaseClasses.add( GuiceUseCase.class );
        useCaseClasses.add( PlainUseCase.class );
        useCaseClasses.add( GuiceUseCase.class );

        for(Class useCaseType : useCaseClasses)
        {
            gc();
            Logger.InstanceCount = 0;
            ErrorHandler.InstanceCount = 0;
            long start=java.lang.System.nanoTime();
            UseCase uc= (UseCase)useCaseType.newInstance();
            uc.Run();
            long end=java.lang.System.nanoTime(); 
            double initTicks = (end-start)*1e-6;
            start=java.lang.System.nanoTime();
            for (int i = 0; i < 100000; i++)
            {
                uc.AddToCheck(uc.Run());
            }
            end=java.lang.System.nanoTime();
            uc.Check();
            double runTicks = (end-start)*1e-6;
            start=java.lang.System.nanoTime();
            uc = (UseCase) useCaseType.newInstance();
            uc.Run();
            end=java.lang.System.nanoTime();
            double init2Ticks = (end-start)*1e-6;
            System.out.println(String.format(Locale.ROOT,"%1$-25s %2$9.2fms init:%3$7.2fms 2nd init:%4$7.2fms ch: %5$d %6$d", useCaseType.getSimpleName(), runTicks, initTicks, init2Ticks, Logger.InstanceCount, ErrorHandler.InstanceCount));
        }
    }
}

