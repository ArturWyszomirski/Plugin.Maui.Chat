namespace Plugin.Maui.Chat.Controls;

public partial class AudioPlayer : ContentView
{
    #region Fields
    Color isStoppedColor;
    readonly Color isPlayingColor = Colors.Green;
    #endregion

    #region Constructor
    public AudioPlayer()
	{
		InitializeComponent();

        isStoppedColor = Color;
        IsVisible = false;

        playerButton.Command = new Command(async () => await StartOrStopAsync());
    }
    #endregion

    #region Bindable properties
    /// <summary>
    /// Holds audio service instance.
    /// </summary>
    internal static readonly BindableProperty AudioServiceProperty =
        BindableProperty.Create(nameof(AudioService), typeof(IAudioService), typeof(AudioPlayer));

    public IAudioService AudioService
    {
        get => (IAudioService)GetValue(AudioServiceProperty);
        set => SetValue(AudioServiceProperty, value);
    }

    /// <summary>
    /// Audio content in message.
    /// </summary>
    public static readonly BindableProperty SourceProperty =
        BindableProperty.Create(nameof(Source), typeof(IAudioSource), typeof(AudioPlayer), defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSourceChanged);

    public IAudioSource Source
    {
        get => (IAudioSource)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    /// <summary>
    /// Audio content button icon.
    /// </summary>
    public static readonly BindableProperty IconProperty =
        BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(AudioPlayer), ImageSource.FromFile(Maui.Chat.Resources.Icons.Waveform));

    public ImageSource Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Audio content button color.
    /// </summary>
    public static readonly BindableProperty ColorProperty =
        BindableProperty.Create(nameof(Color), typeof(Color), typeof(AudioPlayer), default, propertyChanged: OnColorChanged);

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }
    #endregion

    #region Private methods
    private static void OnSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (AudioPlayer)bindable;

        control.OnSourceChanged(newValue);
    }

    private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (AudioPlayer)bindable;

        if (oldValue == default)
            control.isStoppedColor = (Color)newValue;
    }

    private void OnSourceChanged(object newValue) => IsVisible = newValue != null;

    async Task StartOrStopAsync()
    {
        if (!AudioService.IsPlaying)
        {
            Color = isPlayingColor;

            await AudioService.StartPlayingAsync(Source);
        }
        else
        {
            AudioService.StopPlaying();
        }

        Color = isStoppedColor;
    }
    #endregion
}