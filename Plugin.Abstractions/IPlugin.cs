namespace Plugin.Abstractions;

public interface IPlugin
{
    string Name { get; }

    string Initialize();
}