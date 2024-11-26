using Newtonsoft.Json;
using Plugin.Abstractions;

namespace ModuleA
{
    public class ModuleAPlugin : IPlugin
    {
        public string Name => "ModuleA";

        public string Initialize()
        {
            var typeAssembly = typeof(JsonSerializer).Assembly;
            var version = typeAssembly.GetName().Version;
            Console.WriteLine($"ModuleAPlugin loaded. Newtonsoft.Json version: {version}");
            return version.ToString();
        }
    }
}
