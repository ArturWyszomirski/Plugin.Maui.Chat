namespace Plugin.Maui.Chat.Services;

public class TextToSpeechService(Controls.Chat chat)
{
    CancellationTokenSource? cancelReadingTokenSource;

    public async Task StartOrStopReadAsync(string text)
    {
        if (!chat.IsReading)
        {
            cancelReadingTokenSource = new();

            chat.IsReading = true;

            await TextToSpeech.Default.SpeakAsync(text, cancelToken: cancelReadingTokenSource.Token);
        }
        else
        {
            cancelReadingTokenSource?.Cancel();
            chat.IsReading = false;
        }
    }
}
