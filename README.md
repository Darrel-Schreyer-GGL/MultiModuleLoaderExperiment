# MultiModuleLoaderExperiment

Shows how multiple modules using their own versions of NuGet packages can load and uses the specific version referenced by the module.

## WpfAppClient (Uses .NET 8.0 for Windows)

- Load assemblies from the specified module directories.
- Create a new `AssemblyLoadContext` for each module to avoid dependency conflicts.
- Register services from the loaded assemblies.
- Collects instances of `IPlugin` from the loaded modules.
- Passes these plugin instances to the `MainWindow` and displays the main window.
- Display a message with information about the version of `Newtonsoft.Json` used by each plugin.

## WinService (Uses .NET 4.7.2)

- Load assemblies from the specified module directories.
- Create a new `AssemblyLoadContext` for each module to avoid dependency conflicts.
- Register services from the loaded assemblies.
- Collects instances of `IPlugin` from the loaded modules.
- Loops through each plugin and displays a message with information about the version of `Newtonsoft.Json` used by each plugin.

