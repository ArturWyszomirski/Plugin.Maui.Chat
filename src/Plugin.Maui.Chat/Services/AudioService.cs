namespace Plugin.Maui.Chat.Services;

public class AudioService
{
    readonly Controls.Chat chat;

    readonly AudioManager audioManager = new();
    readonly IAudioRecorder audioRecorder;
    AsyncAudioPlayer? audioPlayer;

    public AudioService(Controls.Chat chat)
    {
        this.chat = chat;

        audioRecorder = audioManager.CreateRecorder();
    }

    public async Task<IAudioSource?> StartStopRecorderAsync()
    {
        IAudioSource? audioSource = default;

        if (await Permissions.RequestAsync<Permissions.Microphone>() != PermissionStatus.Granted)
        {
            await Shell.Current.DisplayAlert("Permission denied", "The app needs microphone permission to record audio.", "OK");
            chat.AudioRecorderColor = chat.SecondaryColor;
            return audioSource;
        }

        if (!audioRecorder.IsRecording)
        {
            await audioRecorder.StartAsync();

            chat.IsRecording = true;
            chat.AudioRecorderColor = Colors.Red;

#if ANDROID || WINDOWS
            audioSource = await audioRecorder.StopAsync(When.SilenceIsDetected());
#endif
        }
        else
        {
            audioSource = await audioRecorder.StopAsync(When.Immediately());
        }

        chat.IsRecording = false;
        chat.AudioRecorderColor = chat.PrimaryColor;

        return audioSource;
    }

    public async Task StartStopPlayerAsync(IAudioSource? audioSource)
    {
        if (audioSource == null || audioSource.GetType() == typeof(EmptyAudioSource)) 
            return;

        if (!chat.IsPlaying)
        {
            chat.IsPlaying = true;

            audioPlayer = audioManager.CreateAsyncPlayer(((FileAudioSource)audioSource).GetAudioStream());
            audioPlayer.Volume = 1;

            await audioPlayer.PlayAsync(CancellationToken.None);

            chat.IsPlaying = false;
        }
        else
        {
            audioPlayer?.Stop();

            chat.IsPlaying = false;
        }
    }
}
