using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Plugin.ModuleLoader;

public class ModuleLoader
{
    public List<IPlugin> LoadedModules { get; } = [];

    public void LoadModules()
    {
        Console.WriteLine("Loading Modules");

        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        // Path where the modules are located
        var modulesPath = Path.Combine(
            appDataPath,
            "Gallagher",
            "Security",
            "Modules");

        var assemblies = new List<Assembly>();

        var moduleFolders = Directory.GetDirectories(modulesPath);
        foreach (var moduleFolder in moduleFolders)
        {
            var moduleManifestPath = Path.Combine(moduleFolder, "ModuleManifest.json");
            var moduleManifestJson = File.ReadAllText(moduleManifestPath);
            var moduleManifest = JsonConvert.DeserializeObject<ModuleManifest>(moduleManifestJson);

            var moduleDllPath = Directory.GetFiles(moduleFolder, moduleManifest.AssemblyName).SingleOrDefault();
            if (moduleDllPath == null)
            {
                Console.WriteLine($"No DLL found in {moduleFolder}");
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
                LoadedModules.AddRange(plugins);

                Console.WriteLine($"Loaded module from {moduleDllPath}: {plugins.Count()} plugins found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load or initialize the module from {moduleDllPath}. Error: {ex.Message}");
            }
        }
    }
}
