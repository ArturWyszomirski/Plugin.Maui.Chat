namespace Plugin.Maui.Chat.Controls;

public partial class Chat : ContentView
{
    #region Fields
    static readonly Color _primaryColor = GetPrimaryColor();
    static readonly Color _secondaryColor = GetSecondaryColor();
    #endregion

    #region Constructor
    public Chat()
	{
		InitializeComponent();
    }
    #endregion

    #region Properties
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
        BindableProperty.Create(nameof(SentMessageBackgroundColor), typeof(Color), typeof(Chat), _primaryColor);

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
        BindableProperty.Create(nameof(SentMessageAuthorTextColor), typeof(Color), typeof(Chat), _secondaryColor);

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
        BindableProperty.Create(nameof(SentMessageTimestampTextColor), typeof(Color), typeof(Chat), _secondaryColor);

    public Color SentMessageTimestampTextColor
    {
        get => (Color)GetValue(SentMessageTimestampTextColorProperty);
        set => SetValue(SentMessageTimestampTextColorProperty, value);
    }

    /// <summary>
    /// Sent message content text color.
    /// </summary>
    public static readonly BindableProperty SentMessageContentTextColorProperty = 
        BindableProperty.Create(nameof(SentMessageContentTextColor), typeof(Color), typeof(Chat), _secondaryColor);

    public Color SentMessageContentTextColor
    {
        get => (Color)GetValue(SentMessageContentTextColorProperty);
        set => SetValue(SentMessageContentTextColorProperty, value);
    }
    #endregion

    #region Received message
    /// <summary>
    /// Received message background color.
    /// </summary>
    public static readonly BindableProperty ReceivedMessageBackgroundColorProperty = 
        BindableProperty.Create(nameof(ReceivedMessageBackgroundColor), typeof(Color), typeof(Chat), _secondaryColor);

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
        BindableProperty.Create(nameof(ReceivedMessageAuthorTextColor), typeof(Color), typeof(Chat), _primaryColor);

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
        BindableProperty.Create(nameof(ReceivedMessageTimestampTextColor), typeof(Color), typeof(Chat), _primaryColor);

    public Color ReceivedMessageTimestampTextColor
    {
        get => (Color)GetValue(ReceivedMessageTimestampTextColorProperty);
        set => SetValue(ReceivedMessageTimestampTextColorProperty, value);
    }

    /// <summary>
    /// Received message text color.
    /// </summary>
    public static readonly BindableProperty ReceivedMessageContentTextColorProperty = 
        BindableProperty.Create(nameof(ReceivedMessageContentTextColor), typeof(Color), typeof(Chat), _primaryColor);

    public Color ReceivedMessageContentTextColor
    {
        get => (Color)GetValue(ReceivedMessageContentTextColorProperty);
        set => SetValue(ReceivedMessageContentTextColorProperty, value);
    }
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

    #region User message entry
    /// <summary>
    /// Message typed by user.
    /// </summary>
    public static readonly BindableProperty UserMessageProperty =
        BindableProperty.Create(nameof(UserMessage), typeof(string), typeof(Chat), defaultBindingMode: BindingMode.TwoWay);

    public string UserMessage
    {
        get => (string)GetValue(UserMessageProperty);
        set => SetValue(UserMessageProperty, value);
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

    #region StartStopRecordToggle button
    /// <summary>
    /// Start or stop recording voice message.
    /// </summary>
    public static readonly BindableProperty StartStopRecordToggleCommandProperty = 
        BindableProperty.Create(nameof(StartStopRecordToggleCommand), typeof(ICommand), typeof(Chat));

    public ICommand StartStopRecordToggleCommand
    {
        get => (ICommand)GetValue(StartStopRecordToggleCommandProperty);
        set => SetValue(StartStopRecordToggleCommandProperty, value);
    }

    /// <summary>
    /// Determines whether start/stop record toggle button is visible.
    /// </summary>
    public static readonly BindableProperty IsStartStopRecordToggleVisibleProperty = 
        BindableProperty.Create(nameof(IsStartStopRecordToggleVisible), typeof(bool), typeof(Chat));

    public bool IsStartStopRecordToggleVisible
    {
        get => (bool)GetValue(IsStartStopRecordToggleVisibleProperty);
        set => SetValue(IsStartStopRecordToggleVisibleProperty, value);
    }

    /// <summary>
    /// Start/stop record toggle button icon.
    /// </summary>
    public static readonly BindableProperty StartStopRecordToggleIconProperty = 
        BindableProperty.Create(nameof(StartStopRecordToggleIcon), typeof(ImageSource), typeof(Chat), ImageSource.FromFile(Maui.Chat.Resources.Icons.Microphone));

    public ImageSource StartStopRecordToggleIcon
    {
        get => (ImageSource)GetValue(StartStopRecordToggleIconProperty);
        set => SetValue(StartStopRecordToggleIconProperty, value);
    }

    /// <summary>
    /// Start/stop record toggle button color.
    /// </summary>
    public static readonly BindableProperty StartStopRecordToggleColorProperty = 
        BindableProperty.Create(nameof(StartStopRecordToggleColor), typeof(Color), typeof(Chat), _primaryColor);

    public Color StartStopRecordToggleColor
    {
        get => (Color)GetValue(StartStopRecordToggleColorProperty);
        set => SetValue(StartStopRecordToggleColorProperty, value);
    }
    #endregion

    #region HandsFreeModeToggle button
    /// <summary>
    /// Turn on/off hands-free mode. Hands-free mode is automated recording, transcribing and sending voice messages as well as reading received messages.
    /// </summary>
    public static readonly BindableProperty HandsFreeModeToggleCommandProperty = 
        BindableProperty.Create(nameof(HandsFreeModeToggleCommand), typeof(ICommand), typeof(Chat));

    public ICommand HandsFreeModeToggleCommand
    {
        get => (ICommand)GetValue(HandsFreeModeToggleCommandProperty);
        set => SetValue(HandsFreeModeToggleCommandProperty, value);
    }

    /// <summary>
    /// Determines whether hands-free mode toggle button is visible.
    /// </summary>
    public static readonly BindableProperty IsHandsFreeModeToggleVisibleProperty = 
        BindableProperty.Create(nameof(IsHandsFreeModeToggleVisible), typeof(bool), typeof(Chat));

    public bool IsHandsFreeModeToggleVisible
    {
        get => (bool)GetValue(IsHandsFreeModeToggleVisibleProperty);
        set => SetValue(IsHandsFreeModeToggleVisibleProperty, value);
    }

    /// <summary>
    /// Hands-free mode toggle button icon.
    /// </summary>
    public static readonly BindableProperty HandsFreeModeToggleIconProperty =
        BindableProperty.Create(nameof(HandsFreeModeToggleIcon), typeof(ImageSource), typeof(Chat), ImageSource.FromFile(Maui.Chat.Resources.Icons.Headphones));

    public ImageSource HandsFreeModeToggleIcon
    {
        get => (ImageSource)GetValue(HandsFreeModeToggleIconProperty);
        set => SetValue(HandsFreeModeToggleIconProperty, value);
    }

    /// <summary>
    /// Hands-free mode toggle button color.
    /// </summary>
    public static readonly BindableProperty HandsFreeModeToggleColorProperty = 
        BindableProperty.Create(nameof(HandsFreeModeToggleColor), typeof(Color), typeof(Chat), _primaryColor);

    public Color HandsFreeModeToggleColor
    {
        get => (Color)GetValue(HandsFreeModeToggleColorProperty);
        set => SetValue(HandsFreeModeToggleColorProperty, value);
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
        BindableProperty.Create(nameof(AddAttachmentColor), typeof(Color), typeof(Chat), _primaryColor);

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
        BindableProperty.Create(nameof(TakePhotoColor), typeof(Color), typeof(Chat), _primaryColor);

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
        BindableProperty.Create(nameof(SendMessageColor), typeof(Color), typeof(Chat), _primaryColor);

    public Color SendMessageColor
    {
        get => (Color)GetValue(SendMessageColorProperty);
        set => SetValue(SendMessageColorProperty, value);
    }
    #endregion
    #endregion

    #region Private methods
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

    #region Get default colors
    static Color GetPrimaryColor()
    {
        if (Application.Current == null)
            throw new ArgumentNullException(nameof(Application.Current), $"{nameof(Application.Current)} is null.");

        if (Application.Current.Resources.TryGetValue("Primary", out object color))
            return (Color)color;
        else
            return Colors.Black;
    }
    static Color GetSecondaryColor()
    {
        if (Application.Current == null)
            throw new ArgumentNullException(nameof(Application.Current), $"{nameof(Application.Current)} is null.");

        if (Application.Current.Resources.TryGetValue("Secondary", out object color))
            return (Color)color;
        else
            return Colors.White;
    }
    #endregion
    #endregion
}