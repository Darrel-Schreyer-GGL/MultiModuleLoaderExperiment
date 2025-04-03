using Newtonsoft.Json;
using Plugin.Abstractions;

namespace ModuleA
{
    public class ModuleAPlugin : IPlugin, IPluginVisibility
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
    }
}
