using CommunityToolkit.Mvvm.Input;

namespace Gallagher.Security.Plugin.Abstractions;

public interface IPluginBehavior
{
    public IAsyncRelayCommand ExecuteCommand { get; }
}
