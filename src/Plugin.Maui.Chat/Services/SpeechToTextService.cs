using System.Threading;

namespace Plugin.Maui.Chat.Services;

internal class SpeechToTextService(Controls.Chat chat)
{
    CancellationTokenSource? cancelSpeechToTextTokenSource;

    public async Task<string?> StartOrStopTranscriptionAsync()
    {
        string text = string.Empty;

        if (await Permissions.RequestAsync<Permissions.Microphone>() != PermissionStatus.Granted)
        {
            await Shell.Current.DisplayAlert("Permission denied", "The app needs microphone permission to record audio.", "OK");
            chat.AudioRecorderColor = chat.SecondaryColor;
            return text;
        }

        if (!chat.IsRecording) // couldn't use SpeechToText.Default.CurrentState because is always in Listenig state on Windows OS
        {
            cancelSpeechToTextTokenSource = new();

            chat.IsRecording = true;
            chat.AudioRecorderColor = Colors.Red;

            var recognitionResult = await SpeechToText.Default.ListenAsync(CultureInfo.GetCultureInfo("en-US"), new Progress<string>(partialText =>
            {
                if (!string.IsNullOrWhiteSpace(partialText))
                {
#if WINDOWS
                    text += CapitalizeFirstLetter(partialText) + ". ";
#elif ANDROID
                    text = CapitalizeFirstLetter(partialText) + ". ";
#endif
                }
            }), cancelSpeechToTextTokenSource.Token);

            if (recognitionResult.IsSuccessful)
            {
#if ANDROID
                cancelSpeechToTextTokenSource.Cancel();

                if (!string.IsNullOrWhiteSpace(recognitionResult.Text))
                    text = CapitalizeFirstLetter(recognitionResult.Text) + ". ";
#endif
            }
            else
            {
                await Toast.Make(recognitionResult.Exception?.Message ?? "Unable to recognize speech").Show(CancellationToken.None);
            }
        }
#if ANDROID
        else
            cancelSpeechToTextTokenSource?.Cancel();

        await SpeechToText.Default.DisposeAsync();
#endif

        return text;
    }

    static string CapitalizeFirstLetter(string text) => !string.IsNullOrWhiteSpace(text) ? char.ToUpper(text[0]) + text.Substring(1) : text;
}
