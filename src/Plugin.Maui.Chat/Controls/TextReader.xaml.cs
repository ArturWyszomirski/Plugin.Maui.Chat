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

        readerButton.Command = new Command(async () => await StartOrStopReadingAsync());

        isStoppedColor = Color;
    }
    #endregion

    #region Bindable properties
    /// <summary>
    /// Holds text-to-speech service instance.
    /// </summary>
    internal static readonly BindableProperty TextToSpeechServiceProperty =
        BindableProperty.Create(nameof(TextToSpeechService), typeof(ITextToSpeechService), typeof(TextReader));

    public ITextToSpeechService TextToSpeechService
    {
        get => (ITextToSpeechService)GetValue(TextToSpeechServiceProperty);
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

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }
    #endregion

    #region Private methods
    private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (TextReader)bindable;

        if (oldValue == default)
            control.isStoppedColor = (Color)newValue;
    }

    async Task StartOrStopReadingAsync()
    {
        if (!TextToSpeechService.IsReading)
        {
            Color = isReadingColor;

            await TextToSpeechService.StartReadingAsync(Source);
        }
        else
        {
            TextToSpeechService.StopReading();
        }

        Color = isStoppedColor;
    }
    #endregion
}