using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System.Runtime.Versioning;

namespace YYHEggEgg.Shell;

/// <summary>
/// The provider for creating factory for <see cref="EggEggLogger"/>.
/// </summary>
[UnsupportedOSPlatform("browser")]
[ProviderAlias("EggEgg.Shell")]
public sealed class EggEggLoggerProvider : ILoggerProvider
{
    private readonly string? RootNamespace;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rootNamespace">
    /// If providing your program's root namespace, types whose full name
    /// starting with this will be cut off the prefix to make output more clear.
    /// </param>
    public EggEggLoggerProvider(string? rootNamespace)
    {
        if (rootNamespace?.EndsWith('.') == false)
        {
            rootNamespace = $"{rootNamespace}.";
        }
        if (string.IsNullOrEmpty(rootNamespace))
            RootNamespace = null;
        RootNamespace = rootNamespace;
    }

    /// <inheritdoc/>
    public ILogger CreateLogger(string categoryName)
    {
        return new EggEggLogger(categoryName, RootNamespace);
    }

    /// <inheritdoc/>
    public void Dispose() { }
}

/// <summary>
/// 
/// </summary>
public static class EggEggLoggerExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="rootNamespace">
    /// If providing your program's root namespace, types whose full name
    /// starting with this will be cut off the prefix to make output more clear.
    /// </param>
    /// <returns></returns>
    public static ILoggingBuilder AddEggEggCSharpLogger(this ILoggingBuilder builder, string? rootNamespace = null)
    {
        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, EggEggLoggerProvider>(serviceProvider => new(rootNamespace)));

        return builder;
    }
}