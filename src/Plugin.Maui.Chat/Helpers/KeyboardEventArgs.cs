namespace Plugin.Maui.Chat.Helpers;

public class KeyboardEventArgs
{
    public double KeyboardHeight { get; private set; }

    public KeyboardEventArgs(double keyboardHeight)
    {
        KeyboardHeight = keyboardHeight;
    }
}