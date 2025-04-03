using Plugin.Abstractions;
using Plugin.ModuleLoader;

class Program
{
    private static List<string> _menu = [];

    static void Main(string[] args)
    {
        var moduleLoader = new ModuleLoader();
        moduleLoader.LoadModules();

        Console.WriteLine($"Total plugins loaded: {moduleLoader.LoadedModules.Count}");

        foreach (var plugin in moduleLoader.LoadedModules)
        {
            var version = plugin.Initialize();
            Console.WriteLine($"Plugin: {plugin.Name} - Version: {version}");

            if (plugin is IPluginVisibility pluginVisibility)
            {
                _menu.Add(pluginVisibility.Title);
            }
        }
    }
}