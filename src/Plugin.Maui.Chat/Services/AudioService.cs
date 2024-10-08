﻿namespace Plugin.Maui.Chat.Services;

public partial class AudioService : ObservableRecipient, IAudioService
{
    readonly AudioManager audioManager = new();
    readonly IAudioRecorder audioRecorder;
    AsyncAudioPlayer? audioPlayer;

    public AudioService()
    {
        audioRecorder = audioManager.CreateRecorder();
    }

    [ObservableProperty]
    bool isRecording;

    [ObservableProperty]
    bool isPlaying;

    public async Task<IAudioSource?> StartRecordingUntilSilenceDetectedAsync()
    {
        IAudioSource? audioSource = default;

        if (await Permissions.RequestAsync<Permissions.Microphone>() != PermissionStatus.Granted)
        {
            await Shell.Current.DisplayAlert("Permission denied", "The app needs microphone permission to record audio.", "OK");
            return audioSource;
        }

        if (!audioRecorder.IsRecording)
        {
            IsRecording = true;

            await audioRecorder.StartAsync();

#if ANDROID || WINDOWS
            audioSource = await audioRecorder.StopAsync(When.SilenceIsDetected());
#endif
        }

        IsRecording = false;

        return audioSource;
    }

    public async Task<IAudioSource?> StopRecordingAsync()
    {
        IAudioSource? audioSource = await audioRecorder.StopAsync(When.Immediately());
        
        IsRecording = false;

        return audioSource;
    }

    public async Task StartPlayingAsync(IAudioSource? audioSource)
    {
        if (audioSource == null || audioSource.GetType() == typeof(EmptyAudioSource))
            return;

        audioPlayer = audioManager.CreateAsyncPlayer(((FileAudioSource)audioSource).GetAudioStream());
        audioPlayer.Volume = 1;
        IsPlaying = true;

        await audioPlayer.PlayAsync(CancellationToken.None);

        IsPlaying = false;
    }

    public void StopPlaying()
    {
        audioPlayer?.Stop();

        IsPlaying = false;
    }
}
