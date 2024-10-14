
namespace Plugin.Maui.Chat.Services;

public interface ITextToSpeechService : INotifyPropertyChanged
{
    bool IsReading { get; }

    Task StartReadingAsync(string? text);
    void StopReading();
}