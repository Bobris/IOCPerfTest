class Logger {
    bool verbose = false;
}

class ErrorHandler {
  static int _instanceCount = 0;
  Logger _logger;
  ErrorHandler(Logger this._logger) {
    _instanceCount++;
  }
}

class Database {
  Logger _logger;
  ErrorHandler _handler;
  Database(ErrorHandler this._handler, Logger this._logger);
}

class Authenticator {
  Logger _logger;
  ErrorHandler _handler;
  Database _database;
  Authenticator(Logger this._logger, ErrorHandler this._handler, Database this._database);
}

class StockQuote {
  Logger _logger;
  ErrorHandler _handler;
  Database _database;
  StockQuote(Logger this._logger, ErrorHandler this._handler, Database this._database);
}

class WebService {
  StockQuote _quotes;
  Authenticator _authenticator;
  WebService(Authenticator this._authenticator, StockQuote this._quotes);
  
  void execute() {}
}

void main() {
  Logger logger = new Logger();
  
  var timer = new Stopwatch.start();
  
  List<WebService> array = new List<WebService>();
  for(int i=0; i<100000; i++) {
    WebService service = createWebService(logger);
    array.add(service);
    service.execute();
  }
  
  timer.stop();
  
  print("Elapsed time: " + timer.elapsedInMs());
  print(ErrorHandler._instanceCount);
  
  for (var i = 1; i < array.length; i++) {
    if (array[i]._authenticator._database._handler == array[i - 1]._authenticator._database._handler) print("bad");
    if (array[i]._quotes._database._handler == array[i - 1]._quotes._database._handler) print("bad");
    if (array[i]._quotes._database._handler._logger != array[i - 1]._quotes._database._handler._logger) print("bad");
  }
}

WebService createWebService(Logger logger){
  return new WebService(
    new Authenticator(logger, new ErrorHandler(logger), new Database(new ErrorHandler(logger), logger)),
    new StockQuote(logger, new ErrorHandler(logger), new Database(new ErrorHandler(logger), logger)));
}


