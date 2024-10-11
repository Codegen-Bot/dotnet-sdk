using System;
using CodegenBot;
using Microsoft.Extensions.Logging;

namespace CodegenBot;

public class CustomLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new CustomLogger();
    }

    public void Dispose() { }
}

public class CustomLogger() : ILogger
{
    public IDisposable? BeginScope<TState>(TState state)
        where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter
    )
    {
        Imports.Log(
            new LogEvent()
            {
                Level = logLevel switch
                {
                    LogLevel.Trace => LogEventLevel.Trace,
                    LogLevel.Debug => LogEventLevel.Debug,
                    LogLevel.Information => LogEventLevel.Information,
                    LogLevel.Warning => LogEventLevel.Warning,
                    LogLevel.Error => LogEventLevel.Error,
                    LogLevel.Critical => LogEventLevel.Critical,
                    LogLevel.None => LogEventLevel.Information,
                    _ => LogEventLevel.Information,
                },
                Message = "{Message}",
                Args = [formatter(state, exception)],
            }
        );
    }
}
