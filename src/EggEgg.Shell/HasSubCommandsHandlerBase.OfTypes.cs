using System.Diagnostics.CodeAnalysis;
using CommandLine;
using Microsoft.Extensions.Logging;

namespace YYHEggEgg.Shell;

/// <inheritdoc/>
public abstract class HasSubCommandsHandlerBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption1> : HasSubCommandsHandlerBase
{
    /// <inheritdoc/>
    public HasSubCommandsHandlerBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsHandlerBase(ILogger logger) : base(logger, [typeof(TCmdOption1)])
    {
    }

    /// <summary>
    /// Handler for this subcommand.
    /// </summary>
    /// <param name="o">Option apply to this subcommand.</param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// Command execution will be canceled if:<para/>
    /// - The program is closing (though it's not guaranteed that a graceful cancellation can always be performed);<para/>
    /// - The non-user executor requested this to be canceled.
    /// </param>
    /// <returns></returns>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleAsync(opt),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

#region Powered By SlaveGPT™
/// <inheritdoc/>
public abstract class HasSubCommandsHandlerBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption2> : HasSubCommandsHandlerBase
{
    /// <inheritdoc/>
    public HasSubCommandsHandlerBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsHandlerBase(ILogger logger) : base(logger, [typeof(TCmdOption1), typeof(TCmdOption2)])
    {
    }

    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleAsync(opt),
                async (TCmdOption2 opt) => await HandleAsync(opt),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsHandlerBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption3> : HasSubCommandsHandlerBase
{
    /// <inheritdoc/>
    public HasSubCommandsHandlerBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsHandlerBase(ILogger logger) : base(logger, [typeof(TCmdOption1), typeof(TCmdOption2), typeof(TCmdOption3)])
    {
    }

    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleAsync(opt),
                async (TCmdOption2 opt) => await HandleAsync(opt),
                async (TCmdOption3 opt) => await HandleAsync(opt),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsHandlerBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption4> : HasSubCommandsHandlerBase
{
    /// <inheritdoc/>
    public HasSubCommandsHandlerBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsHandlerBase(ILogger logger) : base(logger,
        [
            typeof(TCmdOption1),
            typeof(TCmdOption2),
            typeof(TCmdOption3),
            typeof(TCmdOption4)
        ])
    {
    }

    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleAsync(opt),
                async (TCmdOption2 opt) => await HandleAsync(opt),
                async (TCmdOption3 opt) => await HandleAsync(opt),
                async (TCmdOption4 opt) => await HandleAsync(opt),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsHandlerBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption5> : HasSubCommandsHandlerBase
{
    /// <inheritdoc/>
    public HasSubCommandsHandlerBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsHandlerBase(ILogger logger) : base(logger,
        [
            typeof(TCmdOption1),
            typeof(TCmdOption2),
            typeof(TCmdOption3),
            typeof(TCmdOption4),
            typeof(TCmdOption5)
        ])
    {
    }

    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleAsync(opt),
                async (TCmdOption2 opt) => await HandleAsync(opt),
                async (TCmdOption3 opt) => await HandleAsync(opt),
                async (TCmdOption4 opt) => await HandleAsync(opt),
                async (TCmdOption5 opt) => await HandleAsync(opt),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsHandlerBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption6> : HasSubCommandsHandlerBase
{
    /// <inheritdoc/>
    public HasSubCommandsHandlerBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsHandlerBase(ILogger logger) : base(logger,
        [
            typeof(TCmdOption1),
            typeof(TCmdOption2),
            typeof(TCmdOption3),
            typeof(TCmdOption4),
            typeof(TCmdOption5),
            typeof(TCmdOption6)
        ])
    {
    }

    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleAsync(opt),
                async (TCmdOption2 opt) => await HandleAsync(opt),
                async (TCmdOption3 opt) => await HandleAsync(opt),
                async (TCmdOption4 opt) => await HandleAsync(opt),
                async (TCmdOption5 opt) => await HandleAsync(opt),
                async (TCmdOption6 opt) => await HandleAsync(opt),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsHandlerBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption7> : HasSubCommandsHandlerBase
{
    /// <inheritdoc/>
    public HasSubCommandsHandlerBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsHandlerBase(ILogger logger) : base(logger,
        [
            typeof(TCmdOption1),
            typeof(TCmdOption2),
            typeof(TCmdOption3),
            typeof(TCmdOption4),
            typeof(TCmdOption5),
            typeof(TCmdOption6),
            typeof(TCmdOption7)
        ])
    {
    }

    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleAsync(opt),
                async (TCmdOption2 opt) => await HandleAsync(opt),
                async (TCmdOption3 opt) => await HandleAsync(opt),
                async (TCmdOption4 opt) => await HandleAsync(opt),
                async (TCmdOption5 opt) => await HandleAsync(opt),
                async (TCmdOption6 opt) => await HandleAsync(opt),
                async (TCmdOption7 opt) => await HandleAsync(opt),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsHandlerBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption8> : HasSubCommandsHandlerBase
{
    /// <inheritdoc/>
    public HasSubCommandsHandlerBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsHandlerBase(ILogger logger) : base(logger,
        [
            typeof(TCmdOption1),
            typeof(TCmdOption2),
            typeof(TCmdOption3),
            typeof(TCmdOption4),
            typeof(TCmdOption5),
            typeof(TCmdOption6),
            typeof(TCmdOption7),
            typeof(TCmdOption8)
        ])
    {
    }

    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleAsync(opt),
                async (TCmdOption2 opt) => await HandleAsync(opt),
                async (TCmdOption3 opt) => await HandleAsync(opt),
                async (TCmdOption4 opt) => await HandleAsync(opt),
                async (TCmdOption5 opt) => await HandleAsync(opt),
                async (TCmdOption6 opt) => await HandleAsync(opt),
                async (TCmdOption7 opt) => await HandleAsync(opt),
                async (TCmdOption8 opt) => await HandleAsync(opt),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsHandlerBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption8, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption9> : HasSubCommandsHandlerBase
{
    /// <inheritdoc/>
    public HasSubCommandsHandlerBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsHandlerBase(ILogger logger) : base(logger,
        [
            typeof(TCmdOption1),
            typeof(TCmdOption2),
            typeof(TCmdOption3),
            typeof(TCmdOption4),
            typeof(TCmdOption5),
            typeof(TCmdOption6),
            typeof(TCmdOption7),
            typeof(TCmdOption8),
            typeof(TCmdOption9)
        ])
    {
    }

    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption9 o, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8, TCmdOption9>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleAsync(opt),
                async (TCmdOption2 opt) => await HandleAsync(opt),
                async (TCmdOption3 opt) => await HandleAsync(opt),
                async (TCmdOption4 opt) => await HandleAsync(opt),
                async (TCmdOption5 opt) => await HandleAsync(opt),
                async (TCmdOption6 opt) => await HandleAsync(opt),
                async (TCmdOption7 opt) => await HandleAsync(opt),
                async (TCmdOption8 opt) => await HandleAsync(opt),
                async (TCmdOption9 opt) => await HandleAsync(opt),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsHandlerBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption8, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption9, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption10> : HasSubCommandsHandlerBase
{
    /// <inheritdoc/>
    public HasSubCommandsHandlerBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsHandlerBase(ILogger logger) : base(logger,
        [
            typeof(TCmdOption1),
            typeof(TCmdOption2),
            typeof(TCmdOption3),
            typeof(TCmdOption4),
            typeof(TCmdOption5),
            typeof(TCmdOption6),
            typeof(TCmdOption7),
            typeof(TCmdOption8),
            typeof(TCmdOption9),
            typeof(TCmdOption10)
        ])
    {
    }

    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption9 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption10 o, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8, TCmdOption9, TCmdOption10>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleAsync(opt),
                async (TCmdOption2 opt) => await HandleAsync(opt),
                async (TCmdOption3 opt) => await HandleAsync(opt),
                async (TCmdOption4 opt) => await HandleAsync(opt),
                async (TCmdOption5 opt) => await HandleAsync(opt),
                async (TCmdOption6 opt) => await HandleAsync(opt),
                async (TCmdOption7 opt) => await HandleAsync(opt),
                async (TCmdOption8 opt) => await HandleAsync(opt),
                async (TCmdOption9 opt) => await HandleAsync(opt),
                async (TCmdOption10 opt) => await HandleAsync(opt),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsHandlerBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption8, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption9, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption10, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption11> : HasSubCommandsHandlerBase
{
    /// <inheritdoc/>
    public HasSubCommandsHandlerBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsHandlerBase(ILogger logger) : base(logger,
        [
            typeof(TCmdOption1),
            typeof(TCmdOption2),
            typeof(TCmdOption3),
            typeof(TCmdOption4),
            typeof(TCmdOption5),
            typeof(TCmdOption6),
            typeof(TCmdOption7),
            typeof(TCmdOption8),
            typeof(TCmdOption9),
            typeof(TCmdOption10),
            typeof(TCmdOption11)
        ])
    {
    }

    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption9 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption10 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption11 o, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8, TCmdOption9, TCmdOption10, TCmdOption11>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleAsync(opt),
                async (TCmdOption2 opt) => await HandleAsync(opt),
                async (TCmdOption3 opt) => await HandleAsync(opt),
                async (TCmdOption4 opt) => await HandleAsync(opt),
                async (TCmdOption5 opt) => await HandleAsync(opt),
                async (TCmdOption6 opt) => await HandleAsync(opt),
                async (TCmdOption7 opt) => await HandleAsync(opt),
                async (TCmdOption8 opt) => await HandleAsync(opt),
                async (TCmdOption9 opt) => await HandleAsync(opt),
                async (TCmdOption10 opt) => await HandleAsync(opt),
                async (TCmdOption11 opt) => await HandleAsync(opt),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsHandlerBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption8, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption9, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption10, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption11, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption12> : HasSubCommandsHandlerBase
{
    /// <inheritdoc/>
    public HasSubCommandsHandlerBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsHandlerBase(ILogger logger) : base(logger,
        [
            typeof(TCmdOption1),
            typeof(TCmdOption2),
            typeof(TCmdOption3),
            typeof(TCmdOption4),
            typeof(TCmdOption5),
            typeof(TCmdOption6),
            typeof(TCmdOption7),
            typeof(TCmdOption8),
            typeof(TCmdOption9),
            typeof(TCmdOption10),
            typeof(TCmdOption11),
            typeof(TCmdOption12)
        ])
    {
    }

    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption9 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption10 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption11 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption12 o, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8, TCmdOption9, TCmdOption10, TCmdOption11, TCmdOption12>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleAsync(opt),
                async (TCmdOption2 opt) => await HandleAsync(opt),
                async (TCmdOption3 opt) => await HandleAsync(opt),
                async (TCmdOption4 opt) => await HandleAsync(opt),
                async (TCmdOption5 opt) => await HandleAsync(opt),
                async (TCmdOption6 opt) => await HandleAsync(opt),
                async (TCmdOption7 opt) => await HandleAsync(opt),
                async (TCmdOption8 opt) => await HandleAsync(opt),
                async (TCmdOption9 opt) => await HandleAsync(opt),
                async (TCmdOption10 opt) => await HandleAsync(opt),
                async (TCmdOption11 opt) => await HandleAsync(opt),
                async (TCmdOption12 opt) => await HandleAsync(opt),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsHandlerBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption8, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption9, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption10, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption11, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption12, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption13> : HasSubCommandsHandlerBase
{
    /// <inheritdoc/>
    public HasSubCommandsHandlerBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsHandlerBase(ILogger logger) : base(logger,
        [
            typeof(TCmdOption1),
            typeof(TCmdOption2),
            typeof(TCmdOption3),
            typeof(TCmdOption4),
            typeof(TCmdOption5),
            typeof(TCmdOption6),
            typeof(TCmdOption7),
            typeof(TCmdOption8),
            typeof(TCmdOption9),
            typeof(TCmdOption10),
            typeof(TCmdOption11),
            typeof(TCmdOption12),
            typeof(TCmdOption13)
        ])
    {
    }

    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption9 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption10 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption11 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption12 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption13 o, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8, TCmdOption9, TCmdOption10, TCmdOption11, TCmdOption12, TCmdOption13>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleAsync(opt),
                async (TCmdOption2 opt) => await HandleAsync(opt),
                async (TCmdOption3 opt) => await HandleAsync(opt),
                async (TCmdOption4 opt) => await HandleAsync(opt),
                async (TCmdOption5 opt) => await HandleAsync(opt),
                async (TCmdOption6 opt) => await HandleAsync(opt),
                async (TCmdOption7 opt) => await HandleAsync(opt),
                async (TCmdOption8 opt) => await HandleAsync(opt),
                async (TCmdOption9 opt) => await HandleAsync(opt),
                async (TCmdOption10 opt) => await HandleAsync(opt),
                async (TCmdOption11 opt) => await HandleAsync(opt),
                async (TCmdOption12 opt) => await HandleAsync(opt),
                async (TCmdOption13 opt) => await HandleAsync(opt),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsHandlerBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption8, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption9, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption10, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption11, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption12, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption13, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption14> : HasSubCommandsHandlerBase
{
    /// <inheritdoc/>
    public HasSubCommandsHandlerBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsHandlerBase(ILogger logger) : base(logger,
        [
            typeof(TCmdOption1),
            typeof(TCmdOption2),
            typeof(TCmdOption3),
            typeof(TCmdOption4),
            typeof(TCmdOption5),
            typeof(TCmdOption6),
            typeof(TCmdOption7),
            typeof(TCmdOption8),
            typeof(TCmdOption9),
            typeof(TCmdOption10),
            typeof(TCmdOption11),
            typeof(TCmdOption12),
            typeof(TCmdOption13),
            typeof(TCmdOption14)
        ])
    {
    }

    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption9 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption10 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption11 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption12 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption13 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption14 o, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8, TCmdOption9, TCmdOption10, TCmdOption11, TCmdOption12, TCmdOption13, TCmdOption14>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleAsync(opt),
                async (TCmdOption2 opt) => await HandleAsync(opt),
                async (TCmdOption3 opt) => await HandleAsync(opt),
                async (TCmdOption4 opt) => await HandleAsync(opt),
                async (TCmdOption5 opt) => await HandleAsync(opt),
                async (TCmdOption6 opt) => await HandleAsync(opt),
                async (TCmdOption7 opt) => await HandleAsync(opt),
                async (TCmdOption8 opt) => await HandleAsync(opt),
                async (TCmdOption9 opt) => await HandleAsync(opt),
                async (TCmdOption10 opt) => await HandleAsync(opt),
                async (TCmdOption11 opt) => await HandleAsync(opt),
                async (TCmdOption12 opt) => await HandleAsync(opt),
                async (TCmdOption13 opt) => await HandleAsync(opt),
                async (TCmdOption14 opt) => await HandleAsync(opt),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsHandlerBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption8, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption9, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption10, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption11, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption12, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption13, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption14, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption15> : HasSubCommandsHandlerBase
{
    /// <inheritdoc/>
    public HasSubCommandsHandlerBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsHandlerBase(ILogger logger) : base(logger,
        [
            typeof(TCmdOption1),
            typeof(TCmdOption2),
            typeof(TCmdOption3),
            typeof(TCmdOption4),
            typeof(TCmdOption5),
            typeof(TCmdOption6),
            typeof(TCmdOption7),
            typeof(TCmdOption8),
            typeof(TCmdOption9),
            typeof(TCmdOption10),
            typeof(TCmdOption11),
            typeof(TCmdOption12),
            typeof(TCmdOption13),
            typeof(TCmdOption14),
            typeof(TCmdOption15)
        ])
    {
    }

    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption9 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption10 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption11 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption12 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption13 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption14 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption15 o, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8, TCmdOption9, TCmdOption10, TCmdOption11, TCmdOption12, TCmdOption13, TCmdOption14, TCmdOption15>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleAsync(opt),
                async (TCmdOption2 opt) => await HandleAsync(opt),
                async (TCmdOption3 opt) => await HandleAsync(opt),
                async (TCmdOption4 opt) => await HandleAsync(opt),
                async (TCmdOption5 opt) => await HandleAsync(opt),
                async (TCmdOption6 opt) => await HandleAsync(opt),
                async (TCmdOption7 opt) => await HandleAsync(opt),
                async (TCmdOption8 opt) => await HandleAsync(opt),
                async (TCmdOption9 opt) => await HandleAsync(opt),
                async (TCmdOption10 opt) => await HandleAsync(opt),
                async (TCmdOption11 opt) => await HandleAsync(opt),
                async (TCmdOption12 opt) => await HandleAsync(opt),
                async (TCmdOption13 opt) => await HandleAsync(opt),
                async (TCmdOption14 opt) => await HandleAsync(opt),
                async (TCmdOption15 opt) => await HandleAsync(opt),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsHandlerBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption8, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption9, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption10, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption11, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption12, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption13, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption14, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption15, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption16> : HasSubCommandsHandlerBase
{
    /// <inheritdoc/>
    public HasSubCommandsHandlerBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsHandlerBase(ILogger logger) : base(logger,
        [
            typeof(TCmdOption1),
            typeof(TCmdOption2),
            typeof(TCmdOption3),
            typeof(TCmdOption4),
            typeof(TCmdOption5),
            typeof(TCmdOption6),
            typeof(TCmdOption7),
            typeof(TCmdOption8),
            typeof(TCmdOption9),
            typeof(TCmdOption10),
            typeof(TCmdOption11),
            typeof(TCmdOption12),
            typeof(TCmdOption13),
            typeof(TCmdOption14),
            typeof(TCmdOption15),
            typeof(TCmdOption16)
        ])
    {
    }

    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption9 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption10 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption11 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption12 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption13 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption14 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption15 o, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsHandlerBase{TCmdOption1}.HandleAsync(TCmdOption1, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption16 o, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8, TCmdOption9, TCmdOption10, TCmdOption11, TCmdOption12, TCmdOption13, TCmdOption14, TCmdOption15, TCmdOption16>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleAsync(opt),
                async (TCmdOption2 opt) => await HandleAsync(opt),
                async (TCmdOption3 opt) => await HandleAsync(opt),
                async (TCmdOption4 opt) => await HandleAsync(opt),
                async (TCmdOption5 opt) => await HandleAsync(opt),
                async (TCmdOption6 opt) => await HandleAsync(opt),
                async (TCmdOption7 opt) => await HandleAsync(opt),
                async (TCmdOption8 opt) => await HandleAsync(opt),
                async (TCmdOption9 opt) => await HandleAsync(opt),
                async (TCmdOption10 opt) => await HandleAsync(opt),
                async (TCmdOption11 opt) => await HandleAsync(opt),
                async (TCmdOption12 opt) => await HandleAsync(opt),
                async (TCmdOption13 opt) => await HandleAsync(opt),
                async (TCmdOption14 opt) => await HandleAsync(opt),
                async (TCmdOption15 opt) => await HandleAsync(opt),
                async (TCmdOption16 opt) => await HandleAsync(opt),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}
#endregion

#region SlaveGPT™'s ending words
// 在C#中，泛型类最多只能有16个类型参数。这是C#语言规范所规定的一个限制。因此，创建一个具有17个泛型参数的类是不符合C#规范的。
//
// 如果你需要处理更多的类型，你可以考虑使用其他方法来实现，例如使用一个泛型基类和多个继承它的非泛型子类，或者使用泛型接口和实现这些接口的非泛型类。另外，也可以考虑使用元组（Tuple）或者字典（Dictionary）来传递和处理更多的类型。
//
// 如果你确实需要支持类似17个不同命令选项这样的场景，你可能需要重新设计你的命令行解析器的架构，以便它不依赖于泛型参数的数量。这可能包括使用反射、动态类型或者构建一个基于策略模式的灵活系统。
#endregion
