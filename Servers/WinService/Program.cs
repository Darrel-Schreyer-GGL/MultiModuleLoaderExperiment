using Microsoft.Extensions.DependencyInjection;
using Plugin.Abstractions;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        // Path where the modules are located
        var modulesPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "..",
            "..",
            "..",
            "..",
            "Modules");

        var assemblies = new List<Assembly>();
        List<IPlugin> loadedModules = new List<IPlugin>();

        var moduleFolders = Directory.GetDirectories(modulesPath);
        foreach (var moduleFolder in moduleFolders)
        {
            var moduleDllFolder = Path.Combine(moduleFolder, "net472"); // Assuming this is the correct folder name for .NET Framework 4.7.2
            var moduleDllPath = Directory.GetFiles(moduleDllFolder, "Module*.dll").FirstOrDefault();
            if (moduleDllPath == null)
            {
                Console.WriteLine($"No DLL found in {moduleDllFolder}");
                continue;
            }

            try
            {
                var assembly = Assembly.LoadFrom(moduleDllPath);
                assemblies.Add(assembly);

                // Create a new service provider for each assembly to avoid dependency conflicts
                var services = new ServiceCollection();
                services.Scan(scan => scan
                    .FromAssemblies(assembly)
                    .AddClasses(classes => classes.AssignableTo<IPlugin>())
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

                var provider = services.BuildServiceProvider();
                var plugins = provider.GetServices<IPlugin>();
                loadedModules.AddRange(plugins);

                Console.WriteLine($"Loaded module from {moduleDllPath}: {plugins.Count()} plugins found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load or initialize the module from {moduleDllPath}. Error: {ex.Message}");
            }
        }

        // Here you can use loadedModules for whatever purpose you intended with your plugins.
        Console.WriteLine($"Total plugins loaded: {loadedModules.Count}");

        foreach (var plugin in loadedModules)
        {
            var version = plugin.Initialize();
            Console.WriteLine($"Plugin: {plugin.Name} - Version: {version}");
        }
    }
}