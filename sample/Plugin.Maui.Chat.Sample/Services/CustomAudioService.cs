namespace Plugin.Maui.Chat.Sample.Services;

internal partial class CustomAudioService : AudioService, IAudioService
{
    readonly AudioManager audioManager = new();

    public new async Task StartPlayingAsync(IAudioSource? audioSource)
    {
        if (audioSource is null or EmptyAudioSource)
        {
            await Shell.Current.DisplayAlert("Custom audio service", "That seems to be empty...", "Oh :(");
        }
        else
        {
            Random random = new();
            double speed = 0.25 + (4 - 0.25) * random.NextDouble();

            await Shell.Current.DisplayAlert("Custom audio service", "Recording will be played with random speed between 0.25 and 4", "Let it be...");

            if (audioSource == null || audioSource.GetType() == typeof(EmptyAudioSource))
                return;

            AsyncAudioPlayer? audioPlayer = audioManager.CreateAsyncPlayer(((FileAudioSource)audioSource).GetAudioStream());
            audioPlayer.SetSpeed(speed);

            IsPlaying = true;

            await audioPlayer.PlayAsync(CancellationToken.None);
        }

        IsPlaying = false;
    }

}
