
namespace Plugin.Maui.Chat.Services;

public interface ISpeechToTextService : INotifyPropertyChanged
{
    bool IsTranscribing { get; }

    Task<string?> StartTranscriptionAsync();
    Task<string> StopTranscriptionAsync();
}