namespace Plugin.Maui.Chat.Services;

public partial class SpeechToTextService : ObservableRecipient, ISpeechToTextService
{
    string text = string.Empty;
    CancellationTokenSource? cancelSpeechToTextTokenSource;

    [ObservableProperty]
    bool isTranscribing;

    public async Task<string?> StartTranscriptionAsync()
    {
        text = string.Empty;

        if (await RequestPermission() is not PermissionStatus.Granted)
            return text;

        cancelSpeechToTextTokenSource = new();

        IsTranscribing = true;

        var recognitionResult = await SpeechToText.Default.ListenAsync(CultureInfo.CurrentCulture, new Progress<string>(partialText =>
        {
            if (!string.IsNullOrWhiteSpace(partialText))
            {
#if WINDOWS 
                text += CapitalizeFirstLetter(partialText) + ". ";
#elif ANDROID || IOS || MACCATALYST
                text = CapitalizeFirstLetter(partialText) + ". ";
#endif
            }
        }), cancelSpeechToTextTokenSource.Token);

        if (recognitionResult.IsSuccessful)
        {
#if ANDROID || IOS || MACCATALYST
            cancelSpeechToTextTokenSource.Cancel();

            if (!string.IsNullOrWhiteSpace(recognitionResult.Text))
                text = CapitalizeFirstLetter(recognitionResult.Text) + ". ";
#endif
        }
        else
        {
            await Toast.Make(recognitionResult.Exception?.Message ?? "Unable to recognize speech").Show(CancellationToken.None);
        }

        IsTranscribing = false;

        return text;
    }

    public async Task<string> StopTranscriptionAsync()
    {
#if ANDROID || IOS || MACCATALYST
        cancelSpeechToTextTokenSource?.Cancel();

        if (cancelSpeechToTextTokenSource?.IsCancellationRequested == false)
            await SpeechToText.Default.DisposeAsync();
#endif

        IsTranscribing = false;

        return text;
    }

    private async Task<PermissionStatus> RequestPermission()
    {
        var isGranted = await SpeechToText.Default.RequestPermissions(CancellationToken.None);
        if (!isGranted)
        {
#if IOS || MACCATALYST
            await Shell.Current.DisplayAlert("Permission denied",
                "This feature requires access to microphone and speech recognition.", "OK");

#elif WINDOWS || ANDROID
            await Shell.Current.DisplayAlert("Permission denied", "The app needs microphone permission to record audio.", "OK");
#endif

            return PermissionStatus.Denied;
        }

        return PermissionStatus.Granted;
    }

    static string CapitalizeFirstLetter(string text) => !string.IsNullOrWhiteSpace(text) ? char.ToUpper(text[0]) + text.Substring(1) : text;
}
