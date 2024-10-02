namespace Plugin.Maui.Chat.Controls;

public partial class TextReader : ContentView
{
    #region Fields
    Color isStoppedColor;
    readonly Color isReadingColor = Colors.Green;
    #endregion

    #region Constructor
    public TextReader()
	{
		InitializeComponent();

        StartStopCommand ??= new Command(async () => await StartOrStopReadingAsync());

        isStoppedColor = Color;
    }
    #endregion

    #region Bindable properties
    /// <summary>
    /// Holds text-to-speech service instance.
    /// </summary>
    internal static readonly BindableProperty TextToSpeechServiceProperty =
        BindableProperty.Create(nameof(TextToSpeechService), typeof(TextToSpeechService), typeof(TextReader));

    public TextToSpeechService TextToSpeechService
    {
        get => (TextToSpeechService)GetValue(TextToSpeechServiceProperty);
        set => SetValue(TextToSpeechServiceProperty, value);
    }

    /// <summary>
    /// Text content in message.
    /// </summary>
    public static readonly BindableProperty SourceProperty =
        BindableProperty.Create(nameof(Source), typeof(string), typeof(TextReader), defaultBindingMode: BindingMode.TwoWay);

    public string Source
    {
        get => (string)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    /// <summary>
    /// Read text message.
    /// </summary>
    public static readonly BindableProperty StartStopCommandProperty =
        BindableProperty.Create(nameof(StartStopCommand), typeof(ICommand), typeof(TextReader), propertyChanged: OnStartStopCommandChanged);

    private static void OnStartStopCommandChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (TextReader)bindable;
        control.OnStartStopCommandChanged(newValue);
    }

    private void OnStartStopCommandChanged(object newValue)
    {
        if (newValue is null) 
            StartStopCommand = new Command(async () => await StartOrStopReadingAsync());
    }

    public ICommand StartStopCommand
    {
        get => (ICommand)GetValue(StartStopCommandProperty);
        set => SetValue(StartStopCommandProperty, value);
    }

    /// <summary>
    /// Read text button icon.
    /// </summary>
    public static readonly BindableProperty IconProperty =
        BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(TextReader), ImageSource.FromFile(Maui.Chat.Resources.Icons.Waveform));

    public ImageSource Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Read text button color.
    /// </summary>
    public static readonly BindableProperty ColorProperty =
        BindableProperty.Create(nameof(Color), typeof(Color), typeof(TextReader), default, propertyChanged: OnColorChanged);

    private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (TextReader)bindable;

        if (oldValue == default)
            control.isStoppedColor = (Color)newValue;
    }

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }
    #endregion

    #region Private methods
    async Task StartOrStopReadingAsync()
    {
        Color = isReadingColor;

        await TextToSpeechService.StartOrStopReadAsync(Source);

        Color = isStoppedColor;
    }
    #endregion
}