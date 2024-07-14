using YYHEggEgg.Logger;

using LogLevel = YYHEggEgg.Logger.LogLevel;
using MSLogLevel = Microsoft.Extensions.Logging.LogLevel;
using CustomLog = YYHEggEgg.Logger.Log;
using Microsoft.Extensions.Logging;

namespace YYHEggEgg.Shell;

/// <summary>
/// A <see cref="ILogger"/> implementation for <see cref="LoggerChannel"/>.
/// </summary>
public class EggEggLogger : ILogger
{
    private readonly LoggerChannel logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="cutPrefix">If <paramref name="sender"/> starts with this, cut it off.</param>
    public EggEggLogger(string? sender, string? cutPrefix = null)
    {
        if (cutPrefix != null)
        {
            if (sender?.StartsWith(cutPrefix) == true)
                sender = sender[cutPrefix.Length..];
        }
        logger = CustomLog.GetChannel(sender);
    }

    /// <inheritdoc/>
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default;

    /// <summary>
    /// Shift between two LogLevel standards.
    /// </summary>
    public static LogLevel Shift(MSLogLevel msloglevel)
    {
        return msloglevel switch
        {
            MSLogLevel.Trace => LogLevel.Verbose,
            MSLogLevel.Debug => LogLevel.Debug,
            MSLogLevel.Information => LogLevel.Information,
            MSLogLevel.Warning => LogLevel.Warning,
            MSLogLevel.Error => LogLevel.Error,
            MSLogLevel.Critical => LogLevel.Error,
            MSLogLevel.None => LogLevel.None,
            _ => throw new NotImplementedException($"This {typeof(MSLogLevel).FullName} is invalid or not supported."),
        };
    }

    /// <summary>
    /// Shift between two LogLevel standards.
    /// </summary>
    public static MSLogLevel Shift(LogLevel msloglevel)
    {
        return msloglevel switch
        {
            LogLevel.Verbose => MSLogLevel.Trace,
            LogLevel.Debug => MSLogLevel.Debug,
            LogLevel.Information => MSLogLevel.Information,
            LogLevel.Warning => MSLogLevel.Warning,
            LogLevel.Error => MSLogLevel.Error,
            LogLevel.None => MSLogLevel.None,
            _ => throw new NotImplementedException($"This {typeof(LogLevel).FullName} is invalid or not supported."),
        };
    }

    /// <inheritdoc/>
    public bool IsEnabled(MSLogLevel logLevel)
    {
        return logger.TargetLogger.CustomConfig.Global_Minimum_LogLevel <= Shift(logLevel);
    }

    /// <inheritdoc/>
    public void Log<TState>(MSLogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (exception == null)
        {
            logger.LogPush(formatter(state, exception), Shift(logLevel));
        }
        else
        {
            logger.LogExceptionTrace(exception, Shift(logLevel), formatter(state, exception));
        }
    }
}

/// <inheritdoc/>
/// <param name="cutPrefix">
/// <inheritdoc cref="EggEggLogger(string?, string?)"/>
/// </param>
public sealed class EggEggLogger<T>(string? cutPrefix = null) : EggEggLogger(typeof(T).FullName, cutPrefix), ILogger<T>
{
}