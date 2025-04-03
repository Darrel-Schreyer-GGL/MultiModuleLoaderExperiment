using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.Input;

namespace Plugin.Abstractions;

public interface IPluginBehavior
{
    public IRelayCommand Command { get; }
}
