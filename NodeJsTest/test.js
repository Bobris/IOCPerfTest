function Logger() {
    this.verbose = false;
};

function ErrorHandler(logger) {
    ErrorHandler.instanceCount++;
	this.logger = logger;
};

ErrorHandler.instanceCount = 0;

function Database(handler, logger) {
	this.handler = handler;
    this.logger = logger;
}

function Authenticator(logger, handler, database) {
    this.logger = logger;
    this.handler = handler;
    this.database = database;
}

function StockQuote(logger, handler, database) {
    this.logger = logger;
    this.handler = handler;
    this.database = database;
}

function WebService(authenticator, quotes) {
	this.authenticator = authenticator;
	this.quotes = quotes;
}

WebService.prototype.execute = function () {
}

var logger = new Logger();
console.time("time");
var array = [];
for (var i = 0; i<100000; i++) {
    var webService = new WebService(
        new Authenticator(logger, new ErrorHandler(logger), new Database(new ErrorHandler(logger), logger)),
        new StockQuote(logger, new ErrorHandler(logger), new Database(new ErrorHandler(logger), logger)));
    array.push(webService);
    webService.execute();
};
console.timeEnd("time");
console.log("%d", ErrorHandler.instanceCount);

for (var i = 1; i < array.length; i++) {
    if (array[i].authenticator.database.handler === array[i - 1].authenticator.database.handler) console.log("bad");
    if (array[i].quotes.database.handler === array[i - 1].quotes.database.handler) console.log("bad");
    if (array[i].quotes.database.handler.logger !== array[i - 1].quotes.database.handler.logger) console.log("bad");
};
