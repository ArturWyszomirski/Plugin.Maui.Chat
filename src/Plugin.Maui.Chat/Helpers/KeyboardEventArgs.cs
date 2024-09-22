namespace Plugin.Maui.Chat.Helpers;

public class KeyboardEventArgs
{
    public double KeyboardHeight { get; internal set; }

    public KeyboardEventArgs(double keyboardHeight)
    {
        KeyboardHeight = keyboardHeight;
    }

    public KeyboardEventArgs()
    {
    }
}