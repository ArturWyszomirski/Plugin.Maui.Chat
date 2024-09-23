using Microsoft.Maui.Controls.Handlers.Items;
using Plugin.Maui.Chat.Controls;
using Plugin.Maui.Chat.Services;
namespace Plugin.Maui.Chat;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .AddAudio()
            .UseMauiCommunityToolkit() // used for tint color in image buttons
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }
}
