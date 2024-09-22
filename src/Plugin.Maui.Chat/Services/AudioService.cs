namespace Plugin.Maui.Chat.Services;

public class AudioService(Controls.Chat chat)
{
    readonly AudioManager audioManager = new();
    AsyncAudioPlayer? audioPlayer;

    public async Task<IAudioSource> StartStopRecorderAsync()
    {
        IAudioSource? audioSource = default;
        IAudioRecorder audioRecorder = audioManager.CreateRecorder();

        if (!audioRecorder.IsRecording)
        {
            await audioRecorder.StartAsync();

            chat.IsRecording = true;

#if ANDROID || WINDOWS
            audioSource = await audioRecorder.StopAsync(When.SilenceIsDetected());
#endif
        }
        else
        {
            audioSource = await audioRecorder.StopAsync(When.Immediately());
        }

        chat.IsRecording = false;

        return audioSource!;
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
