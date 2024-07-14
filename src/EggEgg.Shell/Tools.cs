using CommandLine;
using CommandLine.Text;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using YYHEggEgg.Logger;
using YYHEggEgg.Shell.Attributes;

namespace YYHEggEgg.Shell;

internal class Tools
{
    #region CommandLineParser
    private static SentenceBuilder _defaultFormatError = SentenceBuilder.Create();

    public static IEnumerable<string?> ReportCommandLineErrors(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            yield return _defaultFormatError.FormatError(error);
        }
        yield break;
        // yield return "Unknown command line args detected.";
    }

    public static TextWriter NothingWriter => DropTextWriter.Instance;

    private class DropTextWriter : TextWriter
    {
        public static DropTextWriter Instance = new DropTextWriter();

        public override Encoding Encoding => Encoding.Default;

        public override void Close() { }
        public override void Flush() { }
        public override void Write(bool para_bool) { }
        public override void Write(char para_char) { }
        public override void Write(char[]? para_chars) { }
        public override void Write(char[] para_chars, int para_int1, int para_int2) { }
        public override void Write(decimal para_decimal) { }
        public override void Write(double para_double) { }
        public override void Write(int para_int) { }
        public override void Write(long para_long) { }
        public override void Write(object? para_obj1) { }
        public override void Write(ReadOnlySpan<Char> para_span_char) { }
        public override void Write(Single para_single) { }
        public override void Write(string? para_str) { }
        public override void Write(string? para_str, object? para_obj1) { }
        public override void Write(string? para_str, object? para_obj1, object? para_obj2) { }
        public override void Write(string? para_str, object? para_obj1, object? para_obj2, object? para_obj3) { }
        public override void Write(string? para_str, Object?[] para_objs) { }
        public override void Write(StringBuilder? para_stringBuilder) { }
        public override void Write(uint para_int) { }
        public override void Write(ulong para_long) { }
        public override void WriteLine() { }
        public override void WriteLine(bool para_bool) { }
        public override void WriteLine(char para_char) { }
        public override void WriteLine(char[]? para_chars) { }
        public override void WriteLine(char[] para_chars, int para_int1, int para_int2) { }
        public override void WriteLine(decimal para_decimal) { }
        public override void WriteLine(double para_double) { }
        public override void WriteLine(int para_int) { }
        public override void WriteLine(long para_long) { }
        public override void WriteLine(object? para_obj1) { }
        public override void WriteLine(ReadOnlySpan<Char> para_span_char) { }
        public override void WriteLine(Single para_single) { }
        public override void WriteLine(string? para_str) { }
        public override void WriteLine(string? para_str, object? para_obj1) { }
        public override void WriteLine(string? para_str, object? para_obj1, object? para_obj2) { }
        public override void WriteLine(string? para_str, object? para_obj1, object? para_obj2, object? para_obj3) { }
        public override void WriteLine(string? para_str, Object?[] para_objs) { }
        public override void WriteLine(StringBuilder? para_stringBuilder) { }
        public override void WriteLine(uint para_int) { }
        public override void WriteLine(ulong para_long) { }
    }
    #endregion

    #region RunBackground

#pragma warning disable CS1572 // XML 注释中有 param 标记，但是没有该名称的参数
    /// <summary>
    /// Create a task to run the specified delegate in the background.
    /// </summary>
    /// <param name="action">The delegate to run without any arguments.</param>
    /// <param name="fatal_notice">The part of the message to be passed to <see cref="LoggerExtensions.LogError(ILogger, Exception?, string?, object?[])"/> when the delegate throws an unhandled exception. Typically, a complete sentence followed by the exception information.</param>
    /// <param name="log_sender">The log sender to be passed to the message of <see cref="Log.Erro(string, string?)"/> when the delegate throws an unhandled exception.</param>
    /// <param name="logger">The logger to be passed to the message of <see cref="LoggerExtensions.LogError(ILogger, Exception?, string?, object?[])"/> when the delegate throws an unhandled exception.</param>
    public static void RunBackground(Action action, string fatal_notice, string log_sender = $"{nameof(Tools)}_{nameof(RunBackground)}")
#pragma warning restore CS1572 // XML 注释中有 param 标记，但是没有该名称的参数
    {
        _ = Task.Run(action).ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                LogTrace.ErroTrace(t.Exception, log_sender, fatal_notice);
            }
        }, TaskContinuationOptions.OnlyOnFaulted);
    }

    /// <inheritdoc cref="RunBackground(Action, string, string)"/>
    public static void RunBackground(Func<Task> action, string fatal_notice, string log_sender = $"{nameof(Tools)}_{nameof(RunBackground)}")
    {
        _ = Task.Run(action).ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                LogTrace.ErroTrace(t.Exception, log_sender, fatal_notice);
            }
        }, TaskContinuationOptions.OnlyOnFaulted);
    }

    /// <inheritdoc cref="RunBackground(Action, string, string)"/>
    public static void RunBackground(Action action, string fatal_notice, ILogger logger)
    {
        _ = Task.Run(action).ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                logger.LogError(t.Exception, "{message}", fatal_notice);
            }
        }, TaskContinuationOptions.OnlyOnFaulted);
    }

    /// <inheritdoc cref="RunBackground(Action, string, string)"/>
    public static void RunBackground(Func<Task> action, string fatal_notice, ILogger logger)
    {
        _ = Task.Run(action).ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                logger.LogError(t.Exception, "{message}", fatal_notice);
            }
        }, TaskContinuationOptions.OnlyOnFaulted);
    }
    #endregion

    #region Scan Command Handlers
    [RequiresUnreferencedCode("Require reflection on provided assemblies.")]
    internal static IEnumerable<Type> GetCommandHandlerTypesFromAssemblies(params Assembly?[] assemblies)
    {
        return from assembly in assemblies
               where assembly != null
               from type in assembly.GetTypes()
               where !type.IsAbstract && type.IsAssignableTo(typeof(CommandHandlerBase))
               where type.GetCustomAttribute<DoNotRegisterCommandAttribute>() == null
               select type;
    }

    [RequiresUnreferencedCode("Require reflection on provided assemblies.")]
    internal static IEnumerable<CommandHandlerBase> GetCommandHandlersFromAssemblies(params Assembly?[] assemblies)
    {
        return from type in GetCommandHandlerTypesFromAssemblies(assemblies)
               let ctor = type.GetConstructor([])
               select (CommandHandlerBase)ctor.Invoke(null);
    }

    /// <summary>
    /// Scan all types inherited <see cref="CommandHandlerBase"/> and register them as command handlers.
    /// </summary>
    /// <returns></returns>
    [RequiresUnreferencedCode("Require reflection on Assembly.GetCallingAssembly() and Assembly.GetEntryAssembly().")]
    internal static IEnumerable<CommandHandlerBase> ScanCommandHandlers()
    {
        return GetCommandHandlersFromAssemblies(Assembly.GetCallingAssembly(), Assembly.GetEntryAssembly());
    }
    #endregion
}
