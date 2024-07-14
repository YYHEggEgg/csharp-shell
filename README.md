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
- Type `select -p "` and press `Tab` to see file path auto-completion;
- Press `Ctrl+C` to see exiting prompt. This is customized in [.../CustomCommandLine.cs](src/ShellExample/CustomCommandLine.cs).