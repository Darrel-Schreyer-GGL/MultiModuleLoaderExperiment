namespace Gallagher.Security.Plugin.Abstractions;

public sealed class ModuleManifest
{
    public string ModuleName { get; set; } = default!;
    public string AssemblyName { get; set; } = default!;
}
