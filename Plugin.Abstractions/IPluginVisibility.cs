namespace Plugin.Abstractions;

public interface IPluginVisibility
{
    #nullable enable

    public string Title { get; }
    public string? ImageSource { get; }
    public string? ToolTip { get; }
}
