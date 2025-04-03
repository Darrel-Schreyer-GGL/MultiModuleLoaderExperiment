using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Abstractions;

public interface IPluginReaction
{
    WeakReferenceMessenger PluginMessenger { get; }
}
