namespace Plugin.Maui.Chat.Services;

public interface IAudioService : INotifyPropertyChanged
{
    bool IsRecording { get; }
    bool SoundDetected { get; }
    bool IsPlaying { get; }

    Task<IAudioSource?> StartRecordingUntilSilenceDetectedAsync();
    Task<IAudioSource?> StopRecordingAsync();
    Task StartPlayingAsync(IAudioSource? audioSource);
    void StopPlaying();
}