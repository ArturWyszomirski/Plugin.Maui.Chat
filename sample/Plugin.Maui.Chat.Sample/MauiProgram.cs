using Microsoft.Extensions.Logging;

namespace Plugin.Maui.Chat.Sample;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddTransient<SimpleChatViewModel>();
        builder.Services.AddTransient<CustomizedChatViewModel>();
        builder.Services.AddTransient<AudioChatViewModel>();

        builder.Services.AddTransient<SimpleChatPage>();
        builder.Services.AddTransient<CustomizedChatPage>();
        builder.Services.AddTransient<AudioChatPage>();

        return builder.Build();
    }
}
