using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using Plugin.Abstractions;

namespace ModuleA
{
    public partial class ModuleAPlugin : IPlugin, IPluginVisibility, IPluginBehavior, IPluginReaction
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

        public void RegisterMessenger()
        {
            WeakReferenceMessenger.Default.Register<string>(this, (r, m) =>
            {
                Console.WriteLine($"ModuleAPlugin received message: {m}");
            });
        }
    }
}
