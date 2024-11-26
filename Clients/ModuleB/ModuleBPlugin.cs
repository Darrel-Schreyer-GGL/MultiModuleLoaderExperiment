using Newtonsoft.Json;
using Plugin.Abstractions;

namespace ModuleB
{
    public class ModuleBPlugin : IPlugin
    {
        public string Name => "ModuleB";

        public string Initialize()
        {
            var typeAssembly = typeof(JsonSerializer).Assembly;
            var version = typeAssembly.GetName().Version;
            Console.WriteLine($"ModuleBPlugin loaded. Newtonsoft.Json version: {version}");
            return version.ToString();
        }
    }
}