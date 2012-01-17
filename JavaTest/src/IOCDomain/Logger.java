package IOCDomain;

import com.google.inject.Inject;

public class Logger implements ILogger {
    public static int InstanceCount;
    private boolean _verbose;

    @Inject
    public Logger() {
        InstanceCount++;
    }

    public boolean getVerbose() {
        return _verbose;
    }

    public void setVerbose(boolean value) {
        _verbose = value;
    }
}

