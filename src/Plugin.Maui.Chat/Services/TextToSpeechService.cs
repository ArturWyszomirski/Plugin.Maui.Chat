namespace Plugin.Maui.Chat.Services;

public class TextToSpeechService(Controls.Chat chat)
{
    public CancellationTokenSource? CancelReadingTokenSource { get; private set; }

    public async Task StartOrStopReadAsync(string? text)
    {
        if (string.IsNullOrWhiteSpace(text)) 
            return;

        if (!chat.IsReading)
        {
            CancelReadingTokenSource = new();

            chat.IsReading = true;

            await TextToSpeech.Default.SpeakAsync(text, cancelToken: CancelReadingTokenSource.Token);
        }
        else
        {
            CancelReadingTokenSource?.Cancel();
        }

        chat.IsReading = false;
    }
}
