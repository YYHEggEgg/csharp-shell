using YYHEggEgg.Shell.Example.Hosting.Commands;
using YYHEggEgg.Shell.Example.Hosting;
using YYHEggEgg.Logger;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using YYHEggEgg.Shell;

Log.Initialize(new()
{
    Use_Console_Wrapper = true,
    Use_Working_Directory = true,
    Console_Minimum_LogLevel = YYHEggEgg.Logger.LogLevel.Debug,
    Global_Minimum_LogLevel = YYHEggEgg.Logger.LogLevel.Verbose,
    Debug_LogWriter_AutoFlush = true,
    Max_Output_Char_Count = -1,
    Enable_Detailed_Time = true,
});

var builder = Host.CreateApplicationBuilder(args);

// Change logger
// Type YOUR root namespace here
builder.Logging.UseEggEggLogger("YYHEggEgg.Shell.Example.Hosting");

// Manual register
builder.Services.AddSingleton<CommandHandlerBase, SelectCommand>();
builder.Services.AddSingleton<CommandHandlerBase, HealthCheckCommand>();
// Auto Scan
// builder.Services.AddAllCommands();

builder.Services.AddHostedCommandLineService<CustomCommandLine>();

var app = builder.Build();

await app.RunAsync();
