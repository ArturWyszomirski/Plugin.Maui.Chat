using System;
using Plugin.Maui.Chat.Helpers;

namespace Plugin.Maui.Chat.Services;

public interface IKeyboardService
{
    event EventHandler<KeyboardEventArgs> KeyboardHeightChanged;
    void Start();
    void Stop();
}