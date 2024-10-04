namespace Plugin.Maui.Chat.Services;

public class SpeechToTextService(Controls.Chat chat)
{
    public CancellationTokenSource? CancelSpeechToTextTokenSource { get; private set; }

    public async Task<string?> StartOrStopTranscriptionAsync()
    {
        string text = string.Empty;

        if (await RequestPermission() is not PermissionStatus.Granted)
            return text;
        
        if (!chat.IsRecording) // couldn't use SpeechToText.Default.CurrentState because is always in Listenig state on Windows OS
        {
            CancelSpeechToTextTokenSource = new();

            chat.IsRecording = true;
            chat.AudioRecorderColor = Colors.Red;

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
            }), CancelSpeechToTextTokenSource.Token);

            if (recognitionResult.IsSuccessful)
            {
#if ANDROID || IOS || MACCATALYST
                CancelSpeechToTextTokenSource.Cancel();

                if (!string.IsNullOrWhiteSpace(recognitionResult.Text))
                    text = CapitalizeFirstLetter(recognitionResult.Text) + ". ";
#endif
            }
            else
            {
                await Toast.Make(recognitionResult.Exception?.Message ?? "Unable to recognize speech").Show(CancellationToken.None);
            }
        }
#if ANDROID || IOS || MACCATALYST
        else
            CancelSpeechToTextTokenSource?.Cancel();
        
        if (CancelSpeechToTextTokenSource?.IsCancellationRequested == false)
            await SpeechToText.Default.DisposeAsync();
#endif

        chat.IsRecording = false;
        chat.AudioRecorderColor = chat.PrimaryColor;

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
            chat.AudioRecorderColor = chat.SecondaryColor;
            return PermissionStatus.Denied;
        }

        return PermissionStatus.Granted;
    }

    static string CapitalizeFirstLetter(string text) => !string.IsNullOrWhiteSpace(text) ? char.ToUpper(text[0]) + text.Substring(1) : text;
}
