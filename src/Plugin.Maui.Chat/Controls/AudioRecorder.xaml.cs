namespace Plugin.Maui.Chat.Controls;

public partial class AudioRecorder : ContentView
{
    #region Fields
    Color isStoppedColor;
    readonly Color isRecordingColor = Colors.Red;
    #endregion

    #region Constructor
    public AudioRecorder()
    {
        InitializeComponent();

        isStoppedColor = Color;
        IsVisible = false;

        recorderButton.Command = new Command(async () => await StartOrStopAsync());
    }
    #endregion

    #region Bindable properties
    /// <summary>
    /// Holds audio service instance.
    /// </summary>
    internal static readonly BindableProperty AudioServiceProperty =
        BindableProperty.Create(nameof(AudioService), typeof(IAudioService), typeof(AudioRecorder));

    public IAudioService AudioService
    {
        get => (IAudioService)GetValue(AudioServiceProperty);
        set => SetValue(AudioServiceProperty, value);
    }

    /// <summary>
    /// Holds audio service instance.
    /// </summary>
    internal static readonly BindableProperty SpeechToTextServiceProperty =
        BindableProperty.Create(nameof(SpeechToTextService), typeof(ISpeechToTextService), typeof(AudioRecorder));

    public ISpeechToTextService SpeechToTextService
    {
        get => (ISpeechToTextService)GetValue(SpeechToTextServiceProperty);
        set => SetValue(SpeechToTextServiceProperty, value);
    }

    /// <summary>
    /// Determines whether speech-to-text is enabled.
    /// </summary>
    public static readonly BindableProperty IsSpeechToTextEnabledProperty =
        BindableProperty.Create(nameof(IsSpeechToTextEnabled), typeof(bool), typeof(AudioRecorder));

    public bool IsSpeechToTextEnabled
    {
        get => (bool)GetValue(IsSpeechToTextEnabledProperty);
        set => SetValue(IsSpeechToTextEnabledProperty, value);
    }

    /// <summary>
    /// Audio content in message.
    /// </summary>
    public static readonly BindableProperty AudioContentProperty =
        BindableProperty.Create(nameof(AudioContent), typeof(object), typeof(AudioRecorder), defaultBindingMode: BindingMode.OneWayToSource);

    public object? AudioContent
    {
        get => GetValue(AudioContentProperty);
        set => SetValue(AudioContentProperty, value);
    }

    /// <summary>
    /// Message typed by user.
    /// </summary>
    public static readonly BindableProperty TextContentProperty =
        BindableProperty.Create(nameof(TextContent), typeof(string), typeof(AudioRecorder), defaultBindingMode: BindingMode.OneWayToSource);

    public string? TextContent
    {
        get => (string)GetValue(TextContentProperty);
        set => SetValue(TextContentProperty, value);
    }

    /// <summary>
    /// Audio content button icon.
    /// </summary>
    public static readonly BindableProperty IconProperty =
        BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(AudioRecorder), ImageSource.FromFile(Maui.Chat.Resources.Icons.Microphone));

    public ImageSource Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Audio content button color.
    /// </summary>
    public static readonly BindableProperty ColorProperty =
        BindableProperty.Create(nameof(Color), typeof(Color), typeof(AudioRecorder), default, propertyChanged: OnColorChanged);

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }
    #endregion

    #region Private methods
    private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (AudioRecorder)bindable;

        if (oldValue == default)
            control.isStoppedColor = (Color)newValue;
    }

    async Task StartOrStopAsync()
    {
        if (IsSpeechToTextEnabled)
        {
            if (!SpeechToTextService.IsTranscribing)
            {
                Color = isRecordingColor;

                TextContent += await SpeechToTextService.StartTranscriptionAsync();
            }
            else
                TextContent += await SpeechToTextService.StopTranscriptionAsync();
        }
        else
        {
            if (!AudioService.IsRecording)
            {
                Color = isRecordingColor;

                AudioContent = await AudioService.StartRecordingUntilSilenceDetectedAsync();
            }
            else
            {
                AudioContent = await AudioService.StopRecordingAsync();
            }
        }

        Color = isStoppedColor;
    }
    #endregion
}