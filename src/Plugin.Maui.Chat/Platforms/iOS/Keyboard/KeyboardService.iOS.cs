using System;
using Foundation;
using Plugin.Maui.Chat.Helpers;
using Plugin.Maui.Chat.Services;
using UIKit;

namespace Plugin.Maui.Chat.Platforms.iOS.Keyboard;

public class KeyboardService : IKeyboardService
{
    public event EventHandler<KeyboardEventArgs> KeyboardHeightChanged;
    
    public KeyboardService()
    {
    }
    
    public void Start()
    {
        NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardShow);
        NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardHide);
    }
    
    public void Stop()
    {
        NSNotificationCenter.DefaultCenter.RemoveObserver(UIKeyboard.WillShowNotification);
        NSNotificationCenter.DefaultCenter.RemoveObserver(UIKeyboard.WillHideNotification);
    }
    
    private void OnKeyboardShow(NSNotification notification)
    {
        var keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);
        var keyboardHeight = keyboardFrame.Height;

        KeyboardHeightChanged?.Invoke(this, new KeyboardEventArgs(keyboardHeight));
    }

    private void OnKeyboardHide(NSNotification notification)
    {
        KeyboardHeightChanged?.Invoke(this, new KeyboardEventArgs(0));
    }
}