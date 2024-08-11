# csharp-shell

Provide easy and fully-armed command-based shell experience.

## Features

_(screenshots or records are WIP)_

Just write a command struct in less than 20 lines, and get:

- User input not shuffled by random log output
- Executing-like command line args input
- Auto generate usage
- Auto completion of options or file path while input

You can view example at `src/ShellExample`. `dotnet run` to continue. Try the following:

- Type `help` and see commands overview;
- Type `health` and press `Tab` to see command auto-completion;
- Press `Space`, `Tab` to see subcommand auto-completion, more times to complete options;
- Type `healthcheck help` to see auto-generated usage;
- Type `select -p "` and press `Tab` to see file path auto-completion (the `"` is essential to trigger file-path completion);
- Press `Ctrl+C` to see exiting prompt. This is customized in [.../CustomCommandLine.cs](src/ShellExample/CustomCommandLine.cs).

## How to use?

With no more than 50 lines, you have a basic shell with all features mentioned below. View our example [ShellExample](https://github.com/YYHEggEgg/csharp-shell/blob/master/src/ShellExample):

- It defines a [CustomCommandLine](https://github.com/YYHEggEgg/csharp-shell/blob/master/src/ShellExample/CustomCommandLine.cs) to configure:
  - The welcome notice;
  - The path to store a command history file (and recover from);
  - The exiting notice;
  - Inheriting from `AutoScanMainCommandLine` to auto-register all commands in this assembly.
- Create a file [SelectCommand.cs](https://github.com/YYHEggEgg/csharp-shell/blob/master/src/ShellExample/Commands/SelectCommand.cs) in a separate `Commands` folder, add `using` to `CommandLine` and write a simple option:
  ```cs
  public class SelectOption
  {
      [Option('p', "prefix", Required = false, Default = "", MetaValue = "base-path", HelpText = "The initial prefix.")]
      public required string IncludePrefix { get; set; }
  }
  ```
- Add a class inherited from `StandardCommandHandler<SelectOption>`:
  ```cs
  public class SelectCommand : StandardCommandHandler<SelectOption>
  {
      // ...
  }
  ```
- Use the IDE you love to apply `Fix: Implement Abstract Class`, then fill these overrides:

  ```cs
  public class SelectCommand : StandardCommandHandler<SelectOption>
  {
      public override string CommandName => "select";

      public override string Description => "Query the files status.";

      public override Task<bool> HandleAsync(SelectOption o, CancellationToken cancellationToken)
      {
          _logger.LogInformation("You typed prefix: {prefix}", o.IncludePrefix);
          return Task.FromResult(true);
      }
  }
  ```

- Turn back to [Program.cs](https://github.com/YYHEggEgg/csharp-shell/blob/master/src/ShellExample/Program.cs). We initialize logger, get an instance of `CustomCommandLine` and get it run.
- Try to `dotnet run` this program! You'll see:
  ```log
  09:22:25 <Info:MainCommandLine> This is a common version (suitable for common Program.Main programs).
  09:22:25 <Info:MainCommandLine> Now running! Type 'help' to get commands help.
  09:22:25 <Info:MainCommandLine> ---Running---
  >
  ```
- Try `help` or `?`:
  ```log
  > help
  09:23:07 <Info:select> Command 'select': Query the files status.
  09:23:07 <Info:MainCommandLine> Type [command] help to get more detailed usage.
  09:23:07 <Info:MainCommandLine> ---Running---
  ```
- Try `select help`:
  ```log
  > select help
  09:23:33 <Info:select> Command 'select': Query the files status.
  09:23:33 <Info:select> select [options]
  09:23:33 <Info:select>   [-p, --prefix <base-path>]  The initial prefix.
  09:23:33 <Info:select>
  09:23:33 <Info:MainCommandLine> ---Running---
  ```
  They are all auto-generated from Option Attribute you defined. We can notice that:
  - `-p, --prefix` is generated from the `shortName` and `longName` you gave;
  - `base-path` is from the property `MetaValue`;
  - `The initial prefix` is from `HelpText`;
  - This options is wrapped by `[]` because you mentioned `IsRequired = false`.
- Try execute it:
  ```log
  > select --prefix "EggEgg.Shell"
  09:27:19 <Info:select> You typed prefix: EggEgg.Shell
  09:27:19 <Info:MainCommandLine> ---Running---
  ```

Well done! 

Also, there're examples of:

- [HealthCheckCommand.cs](https://github.com/YYHEggEgg/csharp-shell/blob/master/src/ShellExample/Commands/HealthCheckCommand.cs): A handler with 2 subcommands;
- [Program.cs](https://github.com/YYHEggEgg/csharp-shell/blob/master/src/ShellHostingExample/Program.cs) & [CustomCommandLine.cs](https://github.com/YYHEggEgg/csharp-shell/blob/master/src/ShellHostingExample/CustomCommandLine.cs): Using Command Line as a Hosted Service in a `Microsoft.Extensions.Hosting` project (e.g. ASP.NET server).

## How about more advanced usage?

[EasyProtobuf](https://github.com/YYHEggEgg/EasyProtobuf) is a complex enough project to show you the advanced usage of `EggEgg.Shell`. You can find examples of:

- `EasyProtobuf` supports using a type name (rather than an CommandName) to start handling the specified work. You can see how to enable a handler for non-command general operation at [EasyProtobufCLI.cs (Line 48)](https://github.com/YYHEggEgg/EasyProtobuf/blob/3f1c935686e27c6bb07bc13129d1984deb2f217e/src/MainCLI/EasyProtobufCLI.cs#L48).
- You can see how to define the handler for general operations at [ProtobufHandler.cs (Line 18)](https://github.com/YYHEggEgg/EasyProtobuf/blob/3f1c935686e27c6bb07bc13129d1984deb2f217e/src/MainCLI/ProtobufHandler.cs#L18).
- You can see how to control the usage generation of grouped options (should only have one enabled at a time) at [MT19937Cmd.cs (Line 65)](https://github.com/YYHEggEgg/EasyProtobuf/blob/3f1c935686e27c6bb07bc13129d1984deb2f217e/src/Commands/MT19937Cmd.cs#L65).
- You can see how to add multiple lines for one option at [RsaCmd.Dispatch.cs (Line 167)](https://github.com/YYHEggEgg/EasyProtobuf/blob/3f1c935686e27c6bb07bc13129d1984deb2f217e/src/Commands/Rsa/RsaCmd.Dispatch.cs#L167).
- You can see how to add additional description for one subcommand at [Ec2bCmd.cs (Line 34)](https://github.com/YYHEggEgg/EasyProtobuf/blob/3f1c935686e27c6bb07bc13129d1984deb2f217e/src/Commands/Ec2b/Ec2bCmd.cs#L34).
- [CurrRegionCmd.cs](https://github.com/YYHEggEgg/EasyProtobuf/blob/3f1c935686e27c6bb07bc13129d1984deb2f217e/src/Commands/Dispatch/CurrRegionCmd.cs#L14) defines two handlers that directly inherit from `CommandHandlerBase`, use their own auto-completion rules and `UsageLines`. By the way, you can bypass auto-generation and override `UsageLines` yourself in a common handler, if you don't like it.