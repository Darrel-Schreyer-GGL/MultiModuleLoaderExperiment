using CommunityToolkit.Mvvm.Messaging;

namespace Plugin.Abstractions;

public interface IPluginReaction
{
    WeakReferenceMessenger PluginMessenger { get; }
}