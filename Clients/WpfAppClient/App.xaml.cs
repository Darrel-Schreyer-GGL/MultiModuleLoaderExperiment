using Microsoft.Extensions.DependencyInjection;
using Plugin.Abstractions;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Windows;

namespace WpfAppClient;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Path where the modules are located
        var modulesPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "..",
            "..",
            "..",
            "..",
            "Modules");

        var assemblies = new List<Assembly>();
        List<IPlugin> loadedModules = [];

        var moduleFolders = Directory.GetDirectories(modulesPath);
        foreach (var moduleFolder in moduleFolders)
        {
            var moduleDllFolder = Path.Combine(moduleFolder, "net8.0-windows");
            var moduleDllPath = Directory.GetFiles(moduleDllFolder, "Module*.dll").FirstOrDefault();
            if (moduleDllPath == null)
            {
                continue;
            }

            var alc = new AssemblyLoadContext(moduleFolder, true);

            alc.Resolving += (AssemblyLoadContext context, AssemblyName assemblyName) =>
            {
                var assemblyPath = Path.Combine(moduleDllFolder, $"{assemblyName.Name}.dll");
                if (File.Exists(assemblyPath))
                {
                    return context.LoadFromAssemblyPath(assemblyPath);
                }

                var fallbackAppPath = Path.Combine(Assembly.GetExecutingAssembly().Location, $"{assemblyName.Name}.dll");
                if (File.Exists(fallbackAppPath))
                {
                    return context.LoadFromAssemblyPath(fallbackAppPath);
                }

                return null;
            };

            var assembly = alc.LoadFromAssemblyPath(moduleDllPath);
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
        }

        var mainWindow = new MainWindow();
        mainWindow.SetPlugins(loadedModules);
        mainWindow.Show();
    }
}