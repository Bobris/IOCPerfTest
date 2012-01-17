#include <stdio.h>
#include <iostream>
#include <Windows.h>
#include <vector>

namespace IOCDomain
{
	class ILogger
	{
	};

	class Logger : public ILogger
	{
	public:
		static int InstanceCount;

		Logger()
		{
			InstanceCount++;
		}

		bool Verbose;
	};

	int Logger::InstanceCount = 0;

	class IErrorHandler
	{
	public:
		virtual ILogger * GetLogger() = 0;
	};

	class ErrorHandler : public IErrorHandler
	{
	    ILogger *_logger;

	public:
		static int InstanceCount;

		ErrorHandler(ILogger *logger)
		{
			_logger = logger;
			InstanceCount++;
		}

		virtual ILogger *GetLogger() { return _logger; }
	};

	int ErrorHandler::InstanceCount = 0;

	class IDatabase
	{
	public:
		virtual ILogger *GetLogger() = 0;
		virtual IErrorHandler *GetErrorHandler() = 0;
	};

	class Database : public IDatabase
	{
	    ILogger *_logger;
	    IErrorHandler *_handler;

	public:
		Database(IErrorHandler *handler, ILogger *logger)
		{
			_handler = handler;
            _logger = logger;
        }

		virtual ILogger *GetLogger() { return _logger; }
		virtual IErrorHandler *GetErrorHandler() { return _handler; }
	};
	class IAuthenticator
	{
	public:
		virtual ILogger *GetLogger() = 0;
		virtual IErrorHandler *GetErrorHandler() = 0;
		virtual IDatabase *GetDatabase() = 0;
	};

	class Authenticator : public IAuthenticator
	{
	    ILogger *_logger;
	    IErrorHandler *_handler;
	    IDatabase *_database;

	public:
		Authenticator(ILogger *logger, IErrorHandler *handler, IDatabase *database)
		{
			_logger = logger;
			_handler = handler;
			_database = database;
		}

		virtual ILogger *GetLogger() { return _logger; }
		virtual IErrorHandler *GetErrorHandler() { return _handler; }
		virtual IDatabase *GetDatabase() { return _database; }
	};

	class IStockQuote
	{
	public:
		virtual ILogger *GetLogger() = 0;
		virtual IErrorHandler *GetErrorHandler() = 0;
		virtual IDatabase *GetDatabase() = 0;
	};

	class StockQuote : public IStockQuote
	{
	    ILogger *_logger;
	    IErrorHandler *_handler;
	    IDatabase *_database;

	public:
		StockQuote(ILogger *logger, IErrorHandler *handler, IDatabase *database)
		{
			_logger = logger;
			_handler = handler;
			_database = database;
		}

		virtual ILogger *GetLogger() { return _logger; }
		virtual IErrorHandler *GetErrorHandler() { return _handler; }
		virtual IDatabase *GetDatabase() { return _database; }
	};

	class IWebService
	{
	public:
		virtual IAuthenticator *GetAuthenticator() = 0;
		virtual IStockQuote *StockQuote() = 0;
		virtual void Execute() = 0;
	};

	class WebService : public IWebService
	{
	    IAuthenticator *_authenticator;
	    IStockQuote *_quotes;

	public:
		WebService(IAuthenticator *authenticator, IStockQuote *quotes)
		{
			_authenticator = authenticator;
			_quotes = quotes;
		}

		virtual IAuthenticator *GetAuthenticator() { return _authenticator; }
		virtual IStockQuote *StockQuote() { return _quotes; }

		virtual void Execute()
		{
		}
	};

}

LARGE_INTEGER timerFreq_;
LARGE_INTEGER counterAtStart_;

void startTime()
{
  QueryPerformanceFrequency(&timerFreq_);
  QueryPerformanceCounter(&counterAtStart_);
}

unsigned int calculateElapsedTime()
{
  LARGE_INTEGER c;
  QueryPerformanceCounter(&c);
  return static_cast<unsigned int>( (c.QuadPart - counterAtStart_.QuadPart) * 1000 / timerFreq_.QuadPart );
}

using namespace std;
using namespace IOCDomain;
int main()
{
	ILogger* logger = new IOCDomain::Logger();
	cout << "Start" << endl;
	startTime();
	std::vector<IWebService *> array;
	for (int i = 0; i < 100000; i++)
	{
        IWebService *obj = new WebService(
            new Authenticator(
                logger,
                new ErrorHandler(logger),
                new Database(new ErrorHandler(logger), logger)
                ),
            new StockQuote(
                logger,
                new ErrorHandler(logger),
                new Database(new ErrorHandler(logger), logger)
                )
            );
		array.push_back(obj);
        obj->Execute();
	}
	int time = calculateElapsedTime();
	cout << time << "ms " << Logger::InstanceCount << " " << ErrorHandler::InstanceCount << endl;
	for (int i = 1; i < 100000; i++)
	{
		if (array[i]->GetAuthenticator()->GetDatabase()->GetErrorHandler()==array[i-1]->GetAuthenticator()->GetDatabase()->GetErrorHandler()) cout << "bad";
		if (array[i]->StockQuote()->GetDatabase()->GetErrorHandler()==array[i-1]->StockQuote()->GetDatabase()->GetErrorHandler()) cout << "bad";
		if (array[i]->StockQuote()->GetDatabase()->GetErrorHandler()->GetLogger()!=array[i-1]->StockQuote()->GetDatabase()->GetErrorHandler()->GetLogger()) cout << "bad";
	}
	return 0;
}

