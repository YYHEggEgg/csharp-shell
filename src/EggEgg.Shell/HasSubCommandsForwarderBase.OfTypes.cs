using CommandLine;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace YYHEggEgg.Shell;

/// <inheritdoc/>
public abstract class HasSubCommandsForwarderBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption1> : HasSubCommandsForwarderBase
{
    /// <inheritdoc/>
    public HasSubCommandsForwarderBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsForwarderBase(ILogger logger) : base(logger, [typeof(TCmdOption1)])
    {
    }

    /// <summary>
    /// Forwarder for this subcommand.
    /// </summary>
    /// <param name="o">Forwarding Option apply to this subcommand.</param>
    /// <param name="forwardedCmd">The cmd requested to be forwarded.</param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// Command execution will be canceled if:<para/>
    /// - The program is closing (though it's not guaranteed that a graceful cancellation can always be performed);<para/>
    /// - The non-user executor requested this to be canceled.
    /// </param>
    /// <inheritdoc cref="HandleAsync(string, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default);

    private async Task<bool> HandleForwardShieldAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption1>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
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
public abstract class HasSubCommandsForwarderBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption2> : HasSubCommandsForwarderBase
{
    /// <inheritdoc/>
    public HasSubCommandsForwarderBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsForwarderBase(ILogger logger) : base(logger, [typeof(TCmdOption1), typeof(TCmdOption2)])
    {
    }

    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    private async Task<bool> HandleForwardShieldAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption1>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption2>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption2 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsForwarderBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption3> : HasSubCommandsForwarderBase
{
    /// <inheritdoc/>
    public HasSubCommandsForwarderBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsForwarderBase(ILogger logger) : base(logger, [typeof(TCmdOption1), typeof(TCmdOption2), typeof(TCmdOption3)])
    {
    }

    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    private async Task<bool> HandleForwardShieldAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption1>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption2>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption3>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption2 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption3 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsForwarderBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption4> : HasSubCommandsForwarderBase
{
    /// <inheritdoc/>
    public HasSubCommandsForwarderBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsForwarderBase(ILogger logger) : base(logger, [typeof(TCmdOption1), typeof(TCmdOption2), typeof(TCmdOption3), typeof(TCmdOption4)])
    {
    }

    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    private async Task<bool> HandleForwardShieldAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption1>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption2>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption3>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption4>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption2 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption3 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption4 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsForwarderBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption5> : HasSubCommandsForwarderBase
{
    /// <inheritdoc/>
    public HasSubCommandsForwarderBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsForwarderBase(ILogger logger) : base(logger, new[] { typeof(TCmdOption1), typeof(TCmdOption2), typeof(TCmdOption3), typeof(TCmdOption4), typeof(TCmdOption5) })
    {
    }

    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default);

    private async Task<bool> HandleForwardShieldAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption1>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption2>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption3>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption4>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption5>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption2 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption3 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption4 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption5 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsForwarderBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption6> : HasSubCommandsForwarderBase
{
    /// <inheritdoc/>
    public HasSubCommandsForwarderBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsForwarderBase(ILogger logger) : base(logger, new[] { typeof(TCmdOption1), typeof(TCmdOption2), typeof(TCmdOption3), typeof(TCmdOption4), typeof(TCmdOption5), typeof(TCmdOption6) })
    {
    }

    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default);

    private async Task<bool> HandleForwardShieldAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption1>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption2>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption3>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption4>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption5>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption6>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption2 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption3 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption4 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption5 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption6 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsForwarderBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption7> : HasSubCommandsForwarderBase
{
    /// <inheritdoc/>
    public HasSubCommandsForwarderBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsForwarderBase(ILogger logger) : base(logger, new[] { typeof(TCmdOption1), typeof(TCmdOption2), typeof(TCmdOption3), typeof(TCmdOption4), typeof(TCmdOption5), typeof(TCmdOption6), typeof(TCmdOption7) })
    {
    }

    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    private async Task<bool> HandleForwardShieldAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption1>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption2>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption3>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption4>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption5>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption6>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption7>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption2 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption3 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption4 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption5 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption6 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption7 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsForwarderBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption8> : HasSubCommandsForwarderBase
{
    /// <inheritdoc/>
    public HasSubCommandsForwarderBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsForwarderBase(ILogger logger) : base(logger, new[] { typeof(TCmdOption1), typeof(TCmdOption2), typeof(TCmdOption3), typeof(TCmdOption4), typeof(TCmdOption5), typeof(TCmdOption6), typeof(TCmdOption7), typeof(TCmdOption8) })
    {
    }

    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default);

    private async Task<bool> HandleForwardShieldAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption1>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption2>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption3>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption4>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption5>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption6>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption7>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption8>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption2 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption3 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption4 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption5 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption6 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption7 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption8 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsForwarderBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption8, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption9> : HasSubCommandsForwarderBase
{
    /// <inheritdoc/>
    public HasSubCommandsForwarderBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsForwarderBase(ILogger logger) : base(logger, new[] { typeof(TCmdOption1), typeof(TCmdOption2), typeof(TCmdOption3), typeof(TCmdOption4), typeof(TCmdOption5), typeof(TCmdOption6), typeof(TCmdOption7), typeof(TCmdOption8), typeof(TCmdOption9) })
    {
    }

    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption9 o, string? forwardedCmd, CancellationToken cancellationToken = default);

    private async Task<bool> HandleForwardShieldAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption1>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption2>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption3>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption4>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption5>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption6>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption7>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption8>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption9 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption9>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8, TCmdOption9>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption2 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption3 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption4 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption5 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption6 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption7 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption8 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption9 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsForwarderBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption8, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption9, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption10> : HasSubCommandsForwarderBase
{
    /// <inheritdoc/>
    public HasSubCommandsForwarderBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsForwarderBase(ILogger logger) : base(logger, new[] { typeof(TCmdOption1), typeof(TCmdOption2), typeof(TCmdOption3), typeof(TCmdOption4), typeof(TCmdOption5), typeof(TCmdOption6), typeof(TCmdOption7), typeof(TCmdOption8), typeof(TCmdOption9), typeof(TCmdOption10) })
    {
    }

    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption9 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption10 o, string? forwardedCmd, CancellationToken cancellationToken = default);

    private async Task<bool> HandleForwardShieldAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption1>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption2>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption3>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption4>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption5>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption6>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption7>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption8>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption9 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption9>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption10 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption10>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8, TCmdOption9, TCmdOption10>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption2 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption3 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption4 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption5 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption6 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption7 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption8 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption9 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption10 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsForwarderBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption8, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption9, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption10, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption11> : HasSubCommandsForwarderBase
{
    /// <inheritdoc/>
    public HasSubCommandsForwarderBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsForwarderBase(ILogger logger) : base(logger, new[] { typeof(TCmdOption1), typeof(TCmdOption2), typeof(TCmdOption3), typeof(TCmdOption4), typeof(TCmdOption5), typeof(TCmdOption6), typeof(TCmdOption7), typeof(TCmdOption8), typeof(TCmdOption9), typeof(TCmdOption10), typeof(TCmdOption11) })
    {
    }

    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption9 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption10 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption11 o, string? forwardedCmd, CancellationToken cancellationToken = default);

    private async Task<bool> HandleForwardShieldAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption1>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption2>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption3>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption4>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption5>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption6>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption7>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption8>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption9 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption9>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption10 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption10>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption11 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption11>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8, TCmdOption9, TCmdOption10, TCmdOption11>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption2 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption3 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption4 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption5 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption6 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption7 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption8 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption9 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption10 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption11 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsForwarderBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption8, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption9, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption10, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption11, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption12> : HasSubCommandsForwarderBase
{
    /// <inheritdoc/>
    public HasSubCommandsForwarderBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsForwarderBase(ILogger logger) : base(logger, new[] { typeof(TCmdOption1), typeof(TCmdOption2), typeof(TCmdOption3), typeof(TCmdOption4), typeof(TCmdOption5), typeof(TCmdOption6), typeof(TCmdOption7), typeof(TCmdOption8), typeof(TCmdOption9), typeof(TCmdOption10), typeof(TCmdOption11), typeof(TCmdOption12) })
    {
    }

    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption9 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption10 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption11 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption12 o, string? forwardedCmd, CancellationToken cancellationToken = default);

    private async Task<bool> HandleForwardShieldAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption1>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption2>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption3>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption4>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption5>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption6>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption7>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption8>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption9 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption9>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption10 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption10>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption11 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption11>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption12 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption12>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8, TCmdOption9, TCmdOption10, TCmdOption11, TCmdOption12>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption2 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption3 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption4 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption5 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption6 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption7 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption8 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption9 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption10 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption11 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption12 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}


/// <inheritdoc/>
public abstract class HasSubCommandsForwarderBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption8, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption9, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption10, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption11, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption12, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption13> : HasSubCommandsForwarderBase
{
    /// <inheritdoc/>
    public HasSubCommandsForwarderBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsForwarderBase(ILogger logger) : base(logger, new[] { typeof(TCmdOption1), typeof(TCmdOption2), typeof(TCmdOption3), typeof(TCmdOption4), typeof(TCmdOption5), typeof(TCmdOption6), typeof(TCmdOption7), typeof(TCmdOption8), typeof(TCmdOption9), typeof(TCmdOption10), typeof(TCmdOption11), typeof(TCmdOption12), typeof(TCmdOption13) })
    {
    }

    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption9 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption10 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption11 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption12 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption13 o, string? forwardedCmd, CancellationToken cancellationToken = default);

    private async Task<bool> HandleForwardShieldAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption1>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption2>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption3>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption4>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption5>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption6>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption7>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption8>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption9 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption9>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption10 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption10>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption11 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption11>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption12 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption12>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption13 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption13>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8, TCmdOption9, TCmdOption10, TCmdOption11, TCmdOption12, TCmdOption13>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption2 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption3 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption4 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption5 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption6 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption7 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption8 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption9 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption10 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption11 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption12 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption13 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsForwarderBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption8, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption9, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption10, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption11, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption12, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption13, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption14> : HasSubCommandsForwarderBase
{
    /// <inheritdoc/>
    public HasSubCommandsForwarderBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsForwarderBase(ILogger logger) : base(logger, new[] { typeof(TCmdOption1), typeof(TCmdOption2), typeof(TCmdOption3), typeof(TCmdOption4), typeof(TCmdOption5), typeof(TCmdOption6), typeof(TCmdOption7), typeof(TCmdOption8), typeof(TCmdOption9), typeof(TCmdOption10), typeof(TCmdOption11), typeof(TCmdOption12), typeof(TCmdOption13), typeof(TCmdOption14) })
    {
    }

    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption9 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption10 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption11 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption12 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption13 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption14 o, string? forwardedCmd, CancellationToken cancellationToken = default);

    private async Task<bool> HandleForwardShieldAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption1>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption2>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption3>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption4>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption5>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption6>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption7>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption8>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption9 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption9>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption10 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption10>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption11 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption11>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption12 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption12>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption13 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption13>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption14 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption14>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8, TCmdOption9, TCmdOption10, TCmdOption11, TCmdOption12, TCmdOption13, TCmdOption14>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption2 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption3 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption4 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption5 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption6 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption7 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption8 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption9 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption10 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption11 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption12 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption13 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption14 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsForwarderBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption8, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption9, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption10, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption11, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption12, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption13, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption14, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption15> : HasSubCommandsForwarderBase
{
    /// <inheritdoc/>
    public HasSubCommandsForwarderBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsForwarderBase(ILogger logger) : base(logger, new[] { typeof(TCmdOption1), typeof(TCmdOption2), typeof(TCmdOption3), typeof(TCmdOption4), typeof(TCmdOption5), typeof(TCmdOption6), typeof(TCmdOption7), typeof(TCmdOption8), typeof(TCmdOption9), typeof(TCmdOption10), typeof(TCmdOption11), typeof(TCmdOption12), typeof(TCmdOption13), typeof(TCmdOption14), typeof(TCmdOption15) })
    {
    }

    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption9 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption10 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption11 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption12 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption13 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption14 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption15 o, string? forwardedCmd, CancellationToken cancellationToken = default);

    private async Task<bool> HandleForwardShieldAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption1>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption2>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption3>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption4>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption5>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption6>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption7>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption8>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption9 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption9>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption10 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption10>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption11 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption11>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption12 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption12>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption13 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption13>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption14 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption14>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption15 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption15>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8, TCmdOption9, TCmdOption10, TCmdOption11, TCmdOption12, TCmdOption13, TCmdOption14, TCmdOption15>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption2 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption3 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption4 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption5 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption6 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption7 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption8 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption9 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption10 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption11 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption12 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption13 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption14 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption15 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}

/// <inheritdoc/>
public abstract class HasSubCommandsForwarderBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption1, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption2, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption3, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption4, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption5, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption6, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption7, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption8, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption9, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption10, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption11, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption12, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption13, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption14, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption15, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption16> : HasSubCommandsForwarderBase
{
    /// <inheritdoc/>
    public HasSubCommandsForwarderBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <inheritdoc/>
    public HasSubCommandsForwarderBase(ILogger logger) : base(logger, new[] { typeof(TCmdOption1), typeof(TCmdOption2), typeof(TCmdOption3), typeof(TCmdOption4), typeof(TCmdOption5), typeof(TCmdOption6), typeof(TCmdOption7), typeof(TCmdOption8), typeof(TCmdOption9), typeof(TCmdOption10), typeof(TCmdOption11), typeof(TCmdOption12), typeof(TCmdOption13), typeof(TCmdOption14), typeof(TCmdOption15), typeof(TCmdOption16) })
    {
    }

    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption9 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption10 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption11 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption12 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption13 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption14 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption15 o, string? forwardedCmd, CancellationToken cancellationToken = default);
    /// <inheritdoc cref="HasSubCommandsForwarderBase{TCmdOption1}.HandleAsync(TCmdOption1, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption16 o, string? forwardedCmd, CancellationToken cancellationToken = default);

    private async Task<bool> HandleForwardShieldAsync(TCmdOption1 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption1>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption2 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption2>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption3 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption3>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption4 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption4>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption5 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption5>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption6 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption6>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption7 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption7>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption8 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption8>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption9 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption9>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption10 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption10>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption11 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption11>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption12 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption12>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption13 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption13>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption14 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption14>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption15 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption15>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }
    private async Task<bool> HandleForwardShieldAsync(TCmdOption16 o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield<TCmdOption16>(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        return await DefaultCommandsParser.ParseArguments<TCmdOption1, TCmdOption2, TCmdOption3, TCmdOption4, TCmdOption5, TCmdOption6, TCmdOption7, TCmdOption8, TCmdOption9, TCmdOption10, TCmdOption11, TCmdOption12, TCmdOption13, TCmdOption14, TCmdOption15, TCmdOption16>(args)
            .MapResult(
                async (TCmdOption1 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption2 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption3 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption4 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption5 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption6 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption7 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption8 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption9 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption10 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption11 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption12 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption13 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption14 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption15 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                async (TCmdOption16 opt) => await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken),
                error =>
                {
                    OutputInvalidUsage(error);
                    ShowUsage();
                    return Task.FromResult(false);
                });
    }
}











#endregion