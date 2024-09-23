namespace Plugin.Maui.Chat.Helpers;

internal static class ResourceColors
{
    internal static Color GetPrimaryColor()
    {
        if (Application.Current == null)
            throw new ArgumentNullException(nameof(Application.Current), $"{nameof(Application.Current)} is null.");

        if (Application.Current.Resources.TryGetValue("Primary", out object color))
            return (Color)color;
        else
            return Colors.Black;
    }

    internal static Color GetSecondaryColor()
    {
        if (Application.Current == null)
            throw new ArgumentNullException(nameof(Application.Current), $"{nameof(Application.Current)} is null.");

        if (Application.Current.Resources.TryGetValue("Secondary", out object color))
            return (Color)color;
        else
            return Colors.White;
    }
}
