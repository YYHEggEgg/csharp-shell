// See https://aka.ms/new-console-template for more information
using YYHEggEgg.Logger;
using YYHEggEgg.Shell.Example;

Log.Initialize(new()
{
    Use_Console_Wrapper = true,
    Use_Working_Directory = false,
});

var mainCommandline = new CustomCommandLine();
await mainCommandline.RunAsync();