namespace Plugin.Maui.Chat.Controls;

public partial class Chat : ContentView
{
    #region Fields
    static readonly Color primaryColor = ResourceColors.GetPrimaryColor();
    static readonly Color secondaryColor = ResourceColors.GetSecondaryColor();
    #endregion

    #region Constructor
    public Chat()
	{
		InitializeComponent();

        AudioService = new(this);
        SpeechToTextService = new(this);
        TextToSpeechService = new(this);

        AudioRecorderCommand ??= new Command(async () => await RecordAudioAsync());
    }
    #endregion

    #region Bindable properties
    #region Services
    /// <summary>
    /// Holds audio service instance.
    /// </summary>
    public static readonly BindableProperty AudioServiceProperty =
        BindableProperty.Create(nameof(AudioService), typeof(AudioService), typeof(Chat), defaultBindingMode: BindingMode.OneWayToSource);

    public AudioService AudioService
    {
        get => (AudioService)GetValue(AudioServiceProperty);
        set => SetValue(AudioServiceProperty, value);
    }

    /// <summary>
    /// Holds spech-to-text service instance.
    /// </summary>
    public static readonly BindableProperty SpeechToTextServiceProperty =
        BindableProperty.Create(nameof(SpeechToTextService), typeof(SpeechToTextService), typeof(SpeechToTextService), defaultBindingMode: BindingMode.OneWayToSource);

    public SpeechToTextService SpeechToTextService
    {
        get => (SpeechToTextService)GetValue(SpeechToTextServiceProperty);
        set => SetValue(SpeechToTextServiceProperty, value);
    }

    /// <summary>
    /// Holds text-to-speech service instance.
    /// </summary>
    public static readonly BindableProperty TextToSpeechServiceProperty =
        BindableProperty.Create(nameof(TextToSpeechService), typeof(TextToSpeechService), typeof(TextToSpeechService), defaultBindingMode: BindingMode.OneWayToSource);

    public TextToSpeechService TextToSpeechService
    {
        get => (TextToSpeechService)GetValue(TextToSpeechServiceProperty);
        set => SetValue(TextToSpeechServiceProperty, value);
    }
    #endregion

    #region State properties
    /// <summary>
    /// True when audio recording is on.
    /// </summary>
    public static readonly BindableProperty IsRecordingProperty =
        BindableProperty.Create(nameof(IsRecording), typeof(bool), typeof(Chat), propertyChanged: OnIsRecordingChanged);

    private static void OnIsRecordingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is Chat chat)
            chat.Status = (bool)newValue ? "Recording..." : string.Empty;
    }

    public bool IsRecording
    {
        get => (bool)GetValue(IsRecordingProperty);
        set => SetValue(IsRecordingProperty, value);
    }

    /// <summary>
    /// True when audio playing is on.
    /// </summary>
    public static readonly BindableProperty IsPlayingProperty =
        BindableProperty.Create(nameof(IsPlaying), typeof(bool), typeof(Chat), propertyChanged: OnIsPlayingChanged);

    private static void OnIsPlayingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is Chat chat)
            chat.Status = (bool)newValue ? "Playing..." : string.Empty;
    }

    public bool IsPlaying
    {
        get => (bool)GetValue(IsPlayingProperty);
        set => SetValue(IsPlayingProperty, value);
    }

    /// <summary>
    /// True when text is being read.
    /// </summary>
    public static readonly BindableProperty IsReadingProperty =
        BindableProperty.Create(nameof(IsReading), typeof(bool), typeof(Chat), propertyChanged: OnIsReadingChanged);

    private static void OnIsReadingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is Chat chat)
            chat.Status = (bool)newValue ? "Reading..." : string.Empty;
    }

    public bool IsReading
    {
        get => (bool)GetValue(IsReadingProperty);
        set => SetValue(IsReadingProperty, value);
    }

    /// <summary>
    /// True when text is being read.
    /// </summary>
    public static readonly BindableProperty IsHandsFreeModeOnProperty =
        BindableProperty.Create(nameof(IsHandsFreeModeOn), typeof(bool), typeof(Chat));

    public bool IsHandsFreeModeOn
    {
        get => (bool)GetValue(IsHandsFreeModeOnProperty);
        set => SetValue(IsHandsFreeModeOnProperty, value);
    }
    #endregion

    #region Chat messages collection view
    /// <summary>
    /// List of chat messages.
    /// </summary>
    public static readonly BindableProperty ChatMessagesProperty = 
        BindableProperty.Create(nameof(ChatMessages), typeof(ObservableCollection<ChatMessage>), typeof(Chat), propertyChanged: OnChatMessagesChanged);
	
	public ObservableCollection<ChatMessage> ChatMessages
	{ 
		get => (ObservableCollection<ChatMessage>)GetValue(ChatMessagesProperty); 
		set => SetValue(ChatMessagesProperty, value);
    }

    #region Sent message
    /// <summary>
    /// Sent message background color.
    /// </summary>
    public static readonly BindableProperty SentMessageBackgroundColorProperty = 
        BindableProperty.Create(nameof(SentMessageBackgroundColor), typeof(Color), typeof(Chat), primaryColor);

    public Color SentMessageBackgroundColor
    {
        get => (Color)GetValue(SentMessageBackgroundColorProperty);
        set => SetValue(SentMessageBackgroundColorProperty, value);
    }

    /// <summary>
    /// Sent message author label visibility.
    /// </summary>
    public static readonly BindableProperty IsSentMessageAuthorVisibleProperty = 
        BindableProperty.Create(nameof(IsSentMessageAuthorVisible), typeof(bool), typeof(Chat));

    public bool IsSentMessageAuthorVisible
    {
        get => (bool)GetValue(IsSentMessageAuthorVisibleProperty);
        set => SetValue(IsSentMessageAuthorVisibleProperty, value);
    }

    /// <summary>
    /// Sent message author text color.
    /// </summary>
    public static readonly BindableProperty SentMessageAuthorTextColorProperty = 
        BindableProperty.Create(nameof(SentMessageAuthorTextColor), typeof(Color), typeof(Chat), secondaryColor);

    public Color SentMessageAuthorTextColor
    {
        get => (Color)GetValue(SentMessageAuthorTextColorProperty);
        set => SetValue(SentMessageAuthorTextColorProperty, value);
    }

    /// <summary>
    /// Sent message timestamp label visibility.
    /// </summary>
    public static readonly BindableProperty IsSentMessageTimestampVisibleProperty = 
        BindableProperty.Create(nameof(IsSentMessageTimestampVisible), typeof(bool), typeof(Chat));

    public bool IsSentMessageTimestampVisible
    {
        get => (bool)GetValue(IsSentMessageTimestampVisibleProperty);
        set => SetValue(IsSentMessageTimestampVisibleProperty, value);
    }

    /// <summary>
    /// Sent message timestamp text color.
    /// </summary>
    public static readonly BindableProperty SentMessageTimestampTextColorProperty = 
        BindableProperty.Create(nameof(SentMessageTimestampTextColor), typeof(Color), typeof(Chat), secondaryColor);

    public Color SentMessageTimestampTextColor
    {
        get => (Color)GetValue(SentMessageTimestampTextColorProperty);
        set => SetValue(SentMessageTimestampTextColorProperty, value);
    }

    /// <summary>
    /// Sent message content text color.
    /// </summary>
    public static readonly BindableProperty SentMessageContentTextColorProperty = 
        BindableProperty.Create(nameof(SentMessageContentTextColor), typeof(Color), typeof(Chat), secondaryColor);

    public Color SentMessageContentTextColor
    {
        get => (Color)GetValue(SentMessageContentTextColorProperty);
        set => SetValue(SentMessageContentTextColorProperty, value);
    }

    /// <summary>
    /// Sent audio content button color.
    /// </summary>
    public static readonly BindableProperty SentMessageAudioContentColorProperty =
        BindableProperty.Create(nameof(SentMessageAudioContentColor), typeof(Color), typeof(Chat), secondaryColor);

    public Color SentMessageAudioContentColor
    {
        get => (Color)GetValue(SentMessageAudioContentColorProperty);
        set => SetValue(SentMessageAudioContentColorProperty, value);
    }
    #endregion

    #region Received message
    #region UI properties
    /// <summary>
    /// Received message background color.
    /// </summary>
    public static readonly BindableProperty ReceivedMessageBackgroundColorProperty = 
        BindableProperty.Create(nameof(ReceivedMessageBackgroundColor), typeof(Color), typeof(Chat), secondaryColor);

    public Color ReceivedMessageBackgroundColor
    {
        get => (Color)GetValue(ReceivedMessageBackgroundColorProperty);
        set => SetValue(ReceivedMessageBackgroundColorProperty, value);
    }


    /// <summary>
    /// Received message author label visibility.
    /// </summary>
    public static readonly BindableProperty IsReceivedMessageAuthorVisibleProperty = 
        BindableProperty.Create(nameof(IsReceivedMessageAuthorVisible), typeof(bool), typeof(Chat));

    public bool IsReceivedMessageAuthorVisible
    {
        get => (bool)GetValue(IsReceivedMessageAuthorVisibleProperty);
        set => SetValue(IsReceivedMessageAuthorVisibleProperty, value);
    }

    /// <summary>
    /// Received message author text color.
    /// </summary>
    public static readonly BindableProperty ReceivedMessageAuthorTextColorProperty = 
        BindableProperty.Create(nameof(ReceivedMessageAuthorTextColor), typeof(Color), typeof(Chat), primaryColor);

    public Color ReceivedMessageAuthorTextColor
    {
        get => (Color)GetValue(ReceivedMessageAuthorTextColorProperty);
        set => SetValue(ReceivedMessageAuthorTextColorProperty, value);
    }

    /// <summary>
    /// Received message timestamp label visibility.
    /// </summary>
    public static readonly BindableProperty IsReceivedMessageTimestampVisibleProperty = 
        BindableProperty.Create(nameof(IsReceivedMessageTimestampVisible), typeof(bool), typeof(Chat));

    public bool IsReceivedMessageTimestampVisible
    {
        get => (bool)GetValue(IsReceivedMessageTimestampVisibleProperty);
        set => SetValue(IsReceivedMessageTimestampVisibleProperty, value);
    }

    /// <summary>
    /// Received message timestamp text color.
    /// </summary>
    public static readonly BindableProperty ReceivedMessageTimestampTextColorProperty = 
        BindableProperty.Create(nameof(ReceivedMessageTimestampTextColor), typeof(Color), typeof(Chat), primaryColor);

    public Color ReceivedMessageTimestampTextColor
    {
        get => (Color)GetValue(ReceivedMessageTimestampTextColorProperty);
        set => SetValue(ReceivedMessageTimestampTextColorProperty, value);
    }

    /// <summary>
    /// Received message text color.
    /// </summary>
    public static readonly BindableProperty ReceivedMessageContentTextColorProperty = 
        BindableProperty.Create(nameof(ReceivedMessageContentTextColor), typeof(Color), typeof(Chat), primaryColor);

    public Color ReceivedMessageContentTextColor
    {
        get => (Color)GetValue(ReceivedMessageContentTextColorProperty);
        set => SetValue(ReceivedMessageContentTextColorProperty, value);
    }

    /// <summary>
    /// Received audio content button color.
    /// </summary>
    public static readonly BindableProperty ReceivedMessageAudioContentColorProperty =
        BindableProperty.Create(nameof(ReceivedMessageAudioContentColor), typeof(Color), typeof(Chat), primaryColor);

    public Color ReceivedMessageAudioContentColor
    {
        get => (Color)GetValue(ReceivedMessageAudioContentColorProperty);
        set => SetValue(ReceivedMessageAudioContentColorProperty, value);
    }
    #endregion

    #region Text reader
    /// <summary>
    /// Read or stop text message.
    /// </summary>
    public static readonly BindableProperty TextReaderCommandProperty =
        BindableProperty.Create(nameof(TextReaderCommand), typeof(ICommand), typeof(Chat));

    public ICommand TextReaderCommand
    {
        get => (ICommand)GetValue(TextReaderCommandProperty);
        set => SetValue(TextReaderCommandProperty, value);
    }

    /// <summary>
    /// Text reader button icon.
    /// </summary>
    public static readonly BindableProperty TextReaderIconProperty =
        BindableProperty.Create(nameof(TextReaderIcon), typeof(ImageSource), typeof(Chat), ImageSource.FromFile(Maui.Chat.Resources.Icons.Speaker));

    public ImageSource TextReaderIcon
    {
        get => (ImageSource)GetValue(TextReaderIconProperty);
        set => SetValue(TextReaderIconProperty, value);
    }

    /// <summary>
    /// Text reader button color.
    /// </summary>
    public static readonly BindableProperty TextReaderColorProperty =
        BindableProperty.Create(nameof(TextReaderColor), typeof(Color), typeof(Chat), primaryColor);

    public Color TextReaderColor
    {
        get => (Color)GetValue(TextReaderColorProperty);
        set => SetValue(TextReaderColorProperty, value);
    }

    /// <summary>
    /// Determines whether text-to-speech is enabled.
    /// </summary>
    public static readonly BindableProperty IsTextReaderVisibleProperty =
        BindableProperty.Create(nameof(IsTextReaderVisible), typeof(bool), typeof(Chat));

    public bool IsTextReaderVisible
    {
        get => (bool)GetValue(IsTextReaderVisibleProperty);
        set => SetValue(IsTextReaderVisibleProperty, value);
    }
    #endregion
    #endregion

    #region System message
    /// <summary>
    /// System message background color.
    /// </summary>
    public static readonly BindableProperty SystemMessageBackgroundColorProperty = 
        BindableProperty.Create(nameof(SystemMessageBackgroundColor), typeof(Color), typeof(Chat), Colors.LightGray);

    public Color SystemMessageBackgroundColor
    {
        get => (Color)GetValue(SystemMessageBackgroundColorProperty);
        set => SetValue(SystemMessageBackgroundColorProperty, value);
    }

    /// <summary>
    /// System message text color.
    /// </summary>
    public static readonly BindableProperty SystemMessageTextColorProperty = 
        BindableProperty.Create(nameof(SystemMessageTextColor), typeof(Color), typeof(Chat), Colors.Black);

    public Color SystemMessageTextColor
    {
        get => (Color)GetValue(SystemMessageTextColorProperty);
        set => SetValue(SystemMessageTextColorProperty, value);
    }
    #endregion
    #endregion

    #region User message contents
    /// <summary>
    /// Message typed by user.
    /// </summary>
    public static readonly BindableProperty TextContentProperty =
        BindableProperty.Create(nameof(TextContent), typeof(string), typeof(Chat), defaultBindingMode: BindingMode.TwoWay);

    public string? TextContent
    {
        get => (string)GetValue(TextContentProperty);
        set => SetValue(TextContentProperty, value);
    }

    /// <summary>
    /// Audio content in message.
    /// </summary>
    public static readonly BindableProperty AudioContentProperty =
        BindableProperty.Create(nameof(AudioContent), typeof(object), typeof(Chat), defaultBindingMode: BindingMode.TwoWay);

    public object? AudioContent
    {
        get => GetValue(AudioContentProperty);
        set => SetValue(AudioContentProperty, value);
    }

    /// <summary>
    /// User audio content button color.
    /// </summary>
    public static readonly BindableProperty AudioContentColorProperty =
        BindableProperty.Create(nameof(AudioContentColor), typeof(Color), typeof(Chat), primaryColor);

    public Color AudioContentColor
    {
        get => (Color)GetValue(AudioContentColorProperty);
        set => SetValue(AudioContentColorProperty, value);
    }
    #endregion

    #region Status label
    /// <summary>
    /// Status shown just above the user message entry field e.g. "John Doe is typing..."
    /// </summary>
    public static readonly BindableProperty StatusProperty = 
        BindableProperty.Create(nameof(Status), typeof(string), typeof(Chat));

    public string Status
    {
        get => (string)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    /// <summary>
    /// Determines whether status label is visible.
    /// </summary>
    public static readonly BindableProperty IsStatusVisibleProperty = 
        BindableProperty.Create(nameof(IsStatusVisible), typeof(bool), typeof(Chat));

    public bool IsStatusVisible
    {
        get => (bool)GetValue(IsStatusVisibleProperty);
        set => SetValue(IsStatusVisibleProperty, value);
    }

    /// <summary>
    /// Status text color.
    /// </summary>
    public static readonly BindableProperty StatusTextColorProperty = 
        BindableProperty.Create(nameof(StatusTextColor), typeof(Color), typeof(Chat), Colors.Gray);

    public Color StatusTextColor
    {
        get => (Color)GetValue(StatusTextColorProperty);
        set => SetValue(StatusTextColorProperty, value);
    }
    #endregion

    #region Audio recorder
    /// <summary>
    /// Start or stop recording voice message.
    /// </summary>
    public static readonly BindableProperty AudioRecorderCommandProperty = 
        BindableProperty.Create(nameof(AudioRecorderCommand), typeof(ICommand), typeof(Chat));

    public ICommand AudioRecorderCommand
    {
        get => (ICommand)GetValue(AudioRecorderCommandProperty);
        set => SetValue(AudioRecorderCommandProperty, value);
    }

    /// <summary>
    /// Determines whether start/stop record toggle button is visible.
    /// </summary>
    public static readonly BindableProperty IsAudioRecorderVisibleProperty = 
        BindableProperty.Create(nameof(IsAudioRecorderVisible), typeof(bool), typeof(Chat));

    public bool IsAudioRecorderVisible
    {
        get => (bool)GetValue(IsAudioRecorderVisibleProperty);
        set => SetValue(IsAudioRecorderVisibleProperty, value);
    }

    /// <summary>
    /// Start/stop record toggle button icon.
    /// </summary>
    public static readonly BindableProperty AudioRecorderIconProperty = 
        BindableProperty.Create(nameof(AudioRecorderIcon), typeof(ImageSource), typeof(Chat), ImageSource.FromFile(Maui.Chat.Resources.Icons.Microphone));

    public ImageSource AudioRecorderIcon
    {
        get => (ImageSource)GetValue(AudioRecorderIconProperty);
        set => SetValue(AudioRecorderIconProperty, value);
    }

    /// <summary>
    /// Start/stop record toggle button color.
    /// </summary>
    public static readonly BindableProperty AudioRecorderColorProperty = 
        BindableProperty.Create(nameof(AudioRecorderColor), typeof(Color), typeof(Chat), primaryColor);

    public Color AudioRecorderColor
    {
        get => (Color)GetValue(AudioRecorderColorProperty);
        set => SetValue(AudioRecorderColorProperty, value);
    }
    
    /// <summary>
    /// Determines whether speech-to-text is enabled.
    /// </summary>
    public static readonly BindableProperty IsSpeechToTextEnabledProperty = 
        BindableProperty.Create(nameof(IsSpeechToTextEnabled), typeof(bool), typeof(Chat));

    public bool IsSpeechToTextEnabled
    {
        get => (bool)GetValue(IsSpeechToTextEnabledProperty);
        set => SetValue(IsSpeechToTextEnabledProperty, value);
    }
    #endregion

    #region Audio player
    /// <summary>
    /// Play or stop audio message.
    /// </summary>
    public static readonly BindableProperty AudioPlayerCommandProperty =
        BindableProperty.Create(nameof(AudioPlayerCommand), typeof(ICommand), typeof(Chat));

    public ICommand AudioPlayerCommand
    {
        get => (ICommand)GetValue(AudioPlayerCommandProperty);
        set => SetValue(AudioPlayerCommandProperty, value);
    }

    /// <summary>
    /// Audio content button icon.
    /// </summary>
    public static readonly BindableProperty AudioContentIconProperty =
        BindableProperty.Create(nameof(AudioContentIcon), typeof(ImageSource), typeof(Chat), ImageSource.FromFile(Maui.Chat.Resources.Icons.Waveform));

    public ImageSource AudioContentIcon
    {
        get => (ImageSource)GetValue(AudioContentIconProperty);
        set => SetValue(AudioContentIconProperty, value);
    }
    #endregion

    #region HandsFreeMode button
    /// <summary>
    /// Turn on/off hands-free mode. Hands-free mode is automated recording, transcribing and sending voice messages as well as reading received messages.
    /// </summary>
    public static readonly BindableProperty HandsFreeModeCommandProperty = 
        BindableProperty.Create(nameof(HandsFreeModeCommand), typeof(ICommand), typeof(Chat));

    public ICommand HandsFreeModeCommand
    {
        get => (ICommand)GetValue(HandsFreeModeCommandProperty);
        set => SetValue(HandsFreeModeCommandProperty, value);
    }

    /// <summary>
    /// Determines whether hands-free mode toggle button is visible.
    /// </summary>
    public static readonly BindableProperty IsHandsFreeModeVisibleProperty = 
        BindableProperty.Create(nameof(IsHandsFreeModeVisible), typeof(bool), typeof(Chat));

    public bool IsHandsFreeModeVisible
    {
        get => (bool)GetValue(IsHandsFreeModeVisibleProperty);
        set => SetValue(IsHandsFreeModeVisibleProperty, value);
    }

    /// <summary>
    /// Hands-free mode toggle button icon.
    /// </summary>
    public static readonly BindableProperty HandsFreeModeIconProperty =
        BindableProperty.Create(nameof(HandsFreeModeIcon), typeof(ImageSource), typeof(Chat), ImageSource.FromFile(Maui.Chat.Resources.Icons.Headphones));

    public ImageSource HandsFreeModeIcon
    {
        get => (ImageSource)GetValue(HandsFreeModeIconProperty);
        set => SetValue(HandsFreeModeIconProperty, value);
    }

    /// <summary>
    /// Hands-free mode toggle button color.
    /// </summary>
    public static readonly BindableProperty HandsFreeModeColorProperty = 
        BindableProperty.Create(nameof(HandsFreeModeColor), typeof(Color), typeof(Chat), primaryColor);

    public Color HandsFreeModeColor
    {
        get => (Color)GetValue(HandsFreeModeColorProperty);
        set => SetValue(HandsFreeModeColorProperty, value);
    }
    #endregion

    #region AddAttachment button
    /// <summary>
    /// Turn on/off hands-free mode. Hands-free mode is automated recording, transcribing and sending voice messages as well as reading received messages.
    /// </summary>
    public static readonly BindableProperty AddAttachmentCommandProperty = 
        BindableProperty.Create(nameof(AddAttachmentCommand), typeof(ICommand), typeof(Chat));

    public ICommand AddAttachmentCommand
    {
        get => (ICommand)GetValue(AddAttachmentCommandProperty);
        set => SetValue(AddAttachmentCommandProperty, value);
    }

    /// <summary>
    /// Determines whether add attachment button is visible.
    /// </summary>
    public static readonly BindableProperty IsAddAttachmentVisibleProperty = 
        BindableProperty.Create(nameof(IsAddAttachmentVisible), typeof(bool), typeof(Chat));

    public bool IsAddAttachmentVisible
    {
        get => (bool)GetValue(IsAddAttachmentVisibleProperty);
        set => SetValue(IsAddAttachmentVisibleProperty, value);
    }

    /// <summary>
    /// Add attachment button icon.
    /// </summary>
    public static readonly BindableProperty AddAttachmentIconProperty =
        BindableProperty.Create(nameof(AddAttachmentIcon), typeof(ImageSource), typeof(Chat), ImageSource.FromFile(Maui.Chat.Resources.Icons.PaperClip));

    public ImageSource AddAttachmentIcon
    {
        get => (ImageSource)GetValue(AddAttachmentIconProperty);
        set => SetValue(AddAttachmentIconProperty, value);
    }

    /// <summary>
    /// Add attachment button color.
    /// </summary>
    public static readonly BindableProperty AddAttachmentColorProperty = 
        BindableProperty.Create(nameof(AddAttachmentColor), typeof(Color), typeof(Chat), primaryColor);

    public Color AddAttachmentColor
    {
        get => (Color)GetValue(AddAttachmentColorProperty);
        set => SetValue(AddAttachmentColorProperty, value);
    }
    #endregion

    #region TakePhoto button
    /// <summary>
    /// Turn on/off hands-free mode. Hands-free mode is automated recording, transcribing and sending voice messages as well as reading received messages.
    /// </summary>
    public static readonly BindableProperty TakePhotoCommandProperty = 
        BindableProperty.Create(nameof(TakePhotoCommand), typeof(ICommand), typeof(Chat));

    public ICommand TakePhotoCommand
    {
        get => (ICommand)GetValue(TakePhotoCommandProperty);
        set => SetValue(TakePhotoCommandProperty, value);
    }

    /// <summary>
    /// Determines whether take photo button is visible.
    /// </summary>
    public static readonly BindableProperty IsTakePhotoVisibleProperty = 
        BindableProperty.Create(nameof(IsTakePhotoVisible), typeof(bool), typeof(Chat));

    public bool IsTakePhotoVisible
    {
        get => (bool)GetValue(IsTakePhotoVisibleProperty);
        set => SetValue(IsTakePhotoVisibleProperty, value);
    }

    /// <summary>
    /// Take photo button icon.
    /// </summary>
    public static readonly BindableProperty TakePhotoIconProperty =
        BindableProperty.Create(nameof(TakePhotoIcon), typeof(ImageSource), typeof(Chat), ImageSource.FromFile(Maui.Chat.Resources.Icons.Camera));

    public ImageSource TakePhotoIcon
    {
        get => (ImageSource)GetValue(TakePhotoIconProperty);
        set => SetValue(TakePhotoIconProperty, value);
    }

    /// <summary>
    /// Take photo button color.
    /// </summary>
    public static readonly BindableProperty TakePhotoColorProperty = 
        BindableProperty.Create(nameof(TakePhotoColor), typeof(Color), typeof(Chat), primaryColor);

    public Color TakePhotoColor
    {
        get => (Color)GetValue(TakePhotoColorProperty);
        set => SetValue(TakePhotoColorProperty, value);
    }
    #endregion

    #region SendMessage button
    /// <summary>
    /// Send user message.
    /// </summary>
    public static readonly BindableProperty SendMessageCommandProperty = 
        BindableProperty.Create(nameof(SendMessageCommand), typeof(ICommand), typeof(Chat));

    public ICommand SendMessageCommand
    {
        get => (ICommand)GetValue(SendMessageCommandProperty);
        set => SetValue(SendMessageCommandProperty, value);
    }

    /// <summary>
    /// Determines whether send message button is enabled.
    /// </summary>
    public static readonly BindableProperty IsSendMessageEnabledProperty =
        BindableProperty.Create(nameof(IsSendMessageEnabled), typeof(bool), typeof(Chat));

    public bool IsSendMessageEnabled
    {
        get => (bool)GetValue(IsSendMessageEnabledProperty);
        set => SetValue(IsSendMessageEnabledProperty, value);
    }

    /// <summary>
    /// Determines whether send message button is visible.
    /// </summary>
    public static readonly BindableProperty IsSendMessageVisibleProperty = 
        BindableProperty.Create(nameof(IsSendMessageVisible), typeof(bool), typeof(Chat), true);

    public bool IsSendMessageVisible
    {
        get => (bool)GetValue(IsSendMessageVisibleProperty);
        set => SetValue(IsSendMessageVisibleProperty, value);
    }

    /// <summary>
    /// Send message button icon.
    /// </summary>
    public static readonly BindableProperty SendMessageIconProperty =
        BindableProperty.Create(nameof(SendMessageIcon), typeof(ImageSource), typeof(Chat), ImageSource.FromFile(Maui.Chat.Resources.Icons.PaperPlane));

    public ImageSource SendMessageIcon
    {
        get => (ImageSource)GetValue(SendMessageIconProperty);
        set => SetValue(SendMessageIconProperty, value);
    }

    /// <summary>
    /// Send message button color.
    /// </summary>
    public static readonly BindableProperty SendMessageColorProperty = 
        BindableProperty.Create(nameof(SendMessageColor), typeof(Color), typeof(Chat), primaryColor);

    public Color SendMessageColor
    {
        get => (Color)GetValue(SendMessageColorProperty);
        set => SetValue(SendMessageColorProperty, value);
    }
    #endregion

    #region Style colors
    /// <summary>
    /// Primary chat color.
    /// </summary>
    public static readonly BindableProperty PrimaryColorProperty =
        BindableProperty.Create(nameof(PrimaryColor), typeof(Color), typeof(Chat), primaryColor);

    public Color PrimaryColor
    {
        get => (Color)GetValue(PrimaryColorProperty);
    }

    /// <summary>
    /// Secondary chat color.
    /// </summary>
    public static readonly BindableProperty SecondaryColorProperty =
        BindableProperty.Create(nameof(SecondaryColor), typeof(Color), typeof(Chat), secondaryColor);

    public Color SecondaryColor
    {
        get => (Color)GetValue(SecondaryColorProperty);
    }
    #endregion
    #endregion

    #region Protected methods
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        // workaround for StartStopCommand being null when AudioPlayer resolved (issue #14)
        audioPlayerControl.StartStopCommand = AudioPlayerCommand;
    }
    #endregion

    #region Private methods
    async Task RecordAudioAsync()
    {
        if (await Permissions.RequestAsync<Permissions.Microphone>() != PermissionStatus.Granted)
        {
            await Shell.Current.DisplayAlert("Permission denied", "The app needs microphone permission to record audio.", "OK");
            AudioRecorderColor = SecondaryColor;
            return;
        }

        if (IsSpeechToTextEnabled)
            TextContent += await SpeechToTextService.StartOrStopTranscriptionAsync();
        else
            AudioContent = await AudioService.StartOrStopRecorderAsync();
    }

    #region Workaround for scrolling issue on Android where the last messages were hidden under the device keyboard.
    static void OnChatMessagesChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var chat = (Chat)bindable;

        if (oldValue is ObservableCollection<ChatMessage> oldCollection)
            oldCollection.CollectionChanged -= chat.OnChatMessagesCollectionChanged;

        if (newValue is ObservableCollection<ChatMessage> newCollection)
            newCollection.CollectionChanged += chat.OnChatMessagesCollectionChanged;

        chat.ScrollDownChatMessages();
    }

    void OnChatMessagesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => ScrollDownChatMessages();

    void ScrollDownChatMessages()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Task.Delay(1); // need to be to scroll properly
            await chatMessagesScrollView.ScrollToAsync(chatMessagesCollectionView, ScrollToPosition.End, true);
        });
    }
    #endregion
    #endregion
}