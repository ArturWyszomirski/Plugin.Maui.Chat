namespace Plugin.Maui.Chat.Services;

public partial class TextToSpeechService : ObservableRecipient, ITextToSpeechService
{
    CancellationTokenSource? cancelReadingTokenSource;

    [ObservableProperty]
    public bool isReading;

    public async Task StartReadingAsync(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return;

        cancelReadingTokenSource = new();
        IsReading = true;

        await TextToSpeech.Default.SpeakAsync(text, cancelToken: cancelReadingTokenSource.Token);

        IsReading = false;
    }

    public void StopReading()
    {
        cancelReadingTokenSource?.Cancel();
        IsReading = false;
    }
}
