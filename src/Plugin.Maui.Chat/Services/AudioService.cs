namespace Plugin.Maui.Chat.Services;

internal class AudioService : BaseService
{
    readonly IAudioManager audioManager = new AudioManager();
    readonly IAudioRecorder audioRecorder;
    IAudioSource audioSource;
    AsyncAudioPlayer audioPlayer;

    bool isRecording;

    public AudioService()
    {
        audioRecorder = audioManager.CreateRecorder();
    }

    public bool IsRecording
    {
        get { return isRecording; }
        set 
        { 
            isRecording = value;
            NotifyPropertyChanged();
        }
    }

    internal async Task<IAudioSource> StartStopRecordToggleAsync()
    {
        if (!IsRecording)
        {
            IsRecording = true;

            if (await Permissions.RequestAsync<Permissions.Microphone>() != PermissionStatus.Granted)
            {
                await Shell.Current.DisplayAlert("Permission denied", "The app needs microphone permission to record audio.", "OK");
                return new EmptyAudioSource();
            }

            await audioRecorder.StartAsync();
            audioSource = await audioRecorder.StopAsync(When.SilenceIsDetected());

            IsRecording = false;
        }
        else
        {
            audioSource = await audioRecorder.StopAsync(When.Immediately());

            IsRecording = false;
        }

        return audioSource;
    }
}
