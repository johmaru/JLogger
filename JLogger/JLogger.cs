using Microsoft.Extensions.Logging;
using Utf8StringInterpolation;
using ZLogger;

namespace JLogger;

public class JLogger(ILogger<JLogger> logger)
{
    public async void Log(string[]  message,LogLevel logLevel)
    {
        switch (logLevel)
        {
            case LogLevel.Information:
                foreach (var msg in message)
                {
                    logger.ZLogInformation($"{msg}");
                }
                break;
            case LogLevel.Warning:
                foreach (var msg in message)
                {
                    logger.ZLogWarning($"{msg}");
                }
                break;
            case LogLevel.Error:
                foreach (var msg in message)
                {
                    logger.ZLogError($"{msg}");
                }
                break;
            case LogLevel.Critical:
                foreach (var msg in message)
                {
                    logger.ZLogCritical($"{msg}");
                }
                break;
            case LogLevel.Debug:
                foreach (var msg in message)
                {
                    logger.ZLogDebug($"{msg}");
                }
                break;
        }
    }
}

static class DataTimeExtensions
{
    public static string TimeString()
    {
        DateTime now = DateTime.Now;
        int year = now.Year;
        int month = now.Month;
        int day = now.Day;
        int hour = now.Hour;
        int minute = now.Minute;
        int second = now.Second;
        string timeofYear = year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-" + second;
        
        return timeofYear;
    }
}

public static class FactoryBuilder
{
    public static ILoggerFactory CreateLogger()
    {
        var factory = LoggerFactory.Create(logging =>
        {
            logging.SetMinimumLevel(LogLevel.Trace);
            
            logging.AddZLoggerFile($"./logs/{DataTimeExtensions.TimeString()}-log.txt", options =>
            {
                options.UsePlainTextFormatter(formatter =>
                {
                    formatter.SetPrefixFormatter($"{0}|{1}|",
                        (in MessageTemplate template, in LogInfo info) =>
                            template.Format(info.Timestamp, info.LogLevel));
                    formatter.SetSuffixFormatter($" ({0})",
                        (in MessageTemplate template, in LogInfo info) => template.Format(info.Category));
                    formatter.SetExceptionFormatter((writer, ex) => Utf8String.Format(writer, $"{ex.Message}"));
                });
            });
        });
        return factory;
    }
}