namespace Gallagher.Security.Plugin.Abstractions;

public interface IPluginVisibility
{
    public string Title { get; }
    public string? ImageSource { get; }
    public string? ToolTip { get; }
}
