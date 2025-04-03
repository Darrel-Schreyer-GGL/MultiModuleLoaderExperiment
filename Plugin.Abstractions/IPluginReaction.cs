namespace Plugin.Abstractions;

public interface IPluginReaction
{
    void RegisterMessenger();

    void CallMessenger();
}
