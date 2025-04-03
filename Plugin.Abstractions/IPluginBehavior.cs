using CommunityToolkit.Mvvm.Input;

namespace Plugin.Abstractions;

public interface IPluginBehavior
{
    public IRelayCommand Command { get; }
}
