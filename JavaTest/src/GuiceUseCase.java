import IOCDomain.*;
import com.google.inject.*;

public class GuiceUseCase extends UseCase {
    Injector _container;

    public GuiceUseCase() {
        _container = Guice.createInjector(new Module() {
            @Override public void configure(Binder binder) {
                binder.bind(ILogger.class).to(Logger.class).in(Singleton.class);
                binder.bind(IErrorHandler.class).to(ErrorHandler.class);
                binder.bind(IDatabase.class).to(Database.class);
                binder.bind(IAuthenticator.class).to(Authenticator.class);
                binder.bind(IStockQuote.class).to(StockQuote.class);
                binder.bind(IWebService.class).to(WebService.class);
            }
        });
    }

    @Override
    public IWebService Run() {
        IWebService service = _container.getInstance(IWebService.class);
        service.Execute();
        return service;
    }

}
