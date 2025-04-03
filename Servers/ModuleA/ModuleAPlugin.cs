using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Plugin.Abstractions;

namespace ModuleA
{
    public partial class ModuleAPlugin : IPlugin, IPluginVisibility, IPluginBehavior
    {
        public string Name { get; } = "ModuleA";

        public string Initialize()
        {
            var typeAssembly = typeof(JsonSerializer).Assembly;
            var version = typeAssembly.GetName().Version;
            Console.WriteLine($"ModuleAPlugin loaded. Newtonsoft.Json version: {version}");
            return version.ToString();
        }

        public string Title { get; } = "Module A Title";
        public string? ImageSource { get; }
        public string? ToolTip { get; }

        [RelayCommand]
        public Task Execute()
        {
            Console.WriteLine("ModuleAPlugin executed.");

            return Task.CompletedTask;
        }
    }
}
