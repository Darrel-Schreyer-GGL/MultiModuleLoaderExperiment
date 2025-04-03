using CommunityToolkit.Mvvm.Input;

namespace Plugin.Abstractions;

public interface IPluginBehavior
{
    public IAsyncRelayCommand ExecuteCommand { get; }
}
