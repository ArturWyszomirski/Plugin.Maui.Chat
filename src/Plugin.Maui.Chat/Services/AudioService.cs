namespace Plugin.Maui.Chat.Services;

internal class AudioService
{
    readonly AudioManager audioManager = new AudioManager();
    readonly IAudioRecorder audioRecorder;
    IAudioSource? audioSource;
    AsyncAudioPlayer? audioPlayer;

    readonly Controls.Chat chat;

    public AudioService(Controls.Chat chat)
    {
        this.chat = chat;
        audioRecorder = audioManager.CreateRecorder();
    }

    internal async Task<IAudioSource> StartStopRecordToggleAsync()
    {
        if (!chat.IsRecording)
        {
            chat.IsRecording = true;
            chat.StartStopRecordToggleColor = Colors.Red;
            chat.Status = "Recording...";

            if (await Permissions.RequestAsync<Permissions.Microphone>() != PermissionStatus.Granted)
            {
                await Shell.Current.DisplayAlert("Permission denied", "The app needs microphone permission to record audio.", "OK");
                chat.StartStopRecordToggleColor = chat.SecondaryColor;

                audioSource = new EmptyAudioSource();
            }
            else
            {
                await audioRecorder.StartAsync();
                audioSource = await audioRecorder.StopAsync(When.SilenceIsDetected());
            }
        }
        else
        {
            audioSource = await audioRecorder.StopAsync(When.Immediately());
        }

        chat.IsRecording = false;
        chat.StartStopRecordToggleColor = chat.PrimaryColor;
        chat.Status = string.Empty;
        chat.IsAudioContentVisible = true;

        return audioSource;
    }

    public async Task PlayAudioAsync(IAudioSource? audioSource)
    {
        if (audioSource == null) return;

        if (!chat.IsPlaying)
        {
            audioPlayer = audioManager.CreateAsyncPlayer(((FileAudioSource)audioSource).GetAudioStream());
            
            chat.IsPlaying = true;

            await audioPlayer.PlayAsync(CancellationToken.None);
        }
        else
            audioPlayer?.Stop();

        chat.IsPlaying = false;
    }
}
