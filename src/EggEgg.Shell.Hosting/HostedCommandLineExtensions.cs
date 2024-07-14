﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using YYHEggEgg.Shell.MainCLI;

namespace YYHEggEgg.Shell;

/// <summary>
/// 
/// </summary>
public static class HostedCommandLineExtensions
{
    /// <summary>
    /// Add <typeparamref name="TCLIImpl"/> and <see cref="MainCommandLineHostedService"/>
    /// as singleton, and make <see cref="MainCommandLineHostedService"/> a Hosted Service.
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddHostedCommandLineService<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TCLIImpl>(this IServiceCollection services)
        where TCLIImpl : HostedMainCommandLine
    {
        services.AddSingleton<TCLIImpl>();
        services.AddSingleton<HostedMainCommandLine, TCLIImpl>(serviceProvider => serviceProvider.GetRequiredService<TCLIImpl>());
        services.AddSingleton<MainCommandLineHostedService>();
        services.AddHostedService(serviceProvider => serviceProvider.GetRequiredService<MainCommandLineHostedService>());
        return services;
    }

    /// <summary>
    /// Add <see cref="HostedMainCommandLine"/> and <see cref="MainCommandLineHostedService"/>
    /// as singleton, and make <see cref="MainCommandLineHostedService"/> a Hosted Service.
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddHostedCommandLineService(this IServiceCollection services) =>
        services.AddHostedCommandLineService<HostedMainCommandLine>();

    /// <inheritdoc cref="EggEggLoggerExtensions.AddEggEggCSharpLogger(ILoggingBuilder, string?)"/>
    public static ILoggingBuilder UseEggEggLogger(this ILoggingBuilder logging, string? rootNamespace = null)
    {
        logging.ClearProviders();
        logging.AddEggEggCSharpLogger(rootNamespace);
        return logging;
    }

    /// <summary>
    /// Scan all types in provided <paramref name="assemblies"/>
    /// and register them in the DI container as <see cref="CommandHandlerBase"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies">
    /// The scanning target assemblies list. If not providing, the default
    /// value is <see cref="Assembly.GetEntryAssembly()"/> and <see cref="Assembly.GetCallingAssembly()"/>.
    /// </param>
    /// <returns></returns>
    [RequiresUnreferencedCode("Require reflection on provided assemblies.")]
    public static IServiceCollection AddAllCommands(this IServiceCollection services, params Assembly?[] assemblies)
    {
        if (assemblies == null || assemblies.Length == 0)
        {
            assemblies = [Assembly.GetEntryAssembly(), Assembly.GetCallingAssembly()]; 
        }

        foreach (var handlerType in Tools.GetCommandHandlerTypesFromAssemblies(assemblies))
        {
            services.AddSingleton(typeof(CommandHandlerBase), handlerType);
        }
        return services;
    }
}