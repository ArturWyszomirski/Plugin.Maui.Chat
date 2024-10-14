namespace Plugin.Maui.Chat.Services;

public interface IAudioService : INotifyPropertyChanged
{
    bool IsRecording { get; }
    bool SoundDetected { get; }
    bool IsPlaying { get; }

    Task<IAudioSource?> StartRecordingAsync(bool silenceDetection = true, double silenceTreshold = 2, TimeSpan silenceDuration = default);
    Task<IAudioSource?> StopRecordingAsync();
    Task StartPlayingAsync(IAudioSource? audioSource);
    void StopPlaying();
}