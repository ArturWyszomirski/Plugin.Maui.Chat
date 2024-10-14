namespace Plugin.Maui.Chat.Sample.Services;

internal partial class CustomTextToSpeechService : ObservableRecipient, ITextToSpeechService
{
    [ObservableProperty]
    public bool isReading;

    public async Task StartReadingAsync(string? text)
    {
        IsReading = true;

        await Shell.Current.DisplayAlert("Custom text-to-speech",
                                         $"This is custom text-to-speech service. Instead of being read, the text will be only quouted in this alert:\n\n\"{text}\"",
                                         "Que?");

        IsReading = false;
    }

    public void StopReading()
    {
        return;
    }
}
