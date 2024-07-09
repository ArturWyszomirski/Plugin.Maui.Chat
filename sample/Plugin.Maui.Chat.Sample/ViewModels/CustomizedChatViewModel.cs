namespace Plugin.Maui.Chat.Sample.ViewModels;

public partial class CustomizedChatViewModel : ObservableRecipient
{
    Color? _primaryColor;
    Color? _secondaryColor;

    public CustomizedChatViewModel()
    {
        GetColors();
        InitChatMessages();

        StartStopRecordToggleColor = _secondaryColor;
        HandsFreeModeToggleColor = _secondaryColor;
        AddAttachmentColor = _primaryColor;
        TakePhotoColor = _primaryColor;
        SendMessageColor = _primaryColor;
    }

    [ObservableProperty]
    string? _userMessage;

    [ObservableProperty]
    string? _status;

    [ObservableProperty]
    bool _isStatusVisible;

    [ObservableProperty]
    Color? _startStopRecordToggleColor;

    [ObservableProperty]
    Color? _handsFreeModeToggleColor;

    [ObservableProperty]
    Color? _addAttachmentColor;

    [ObservableProperty]
    Color? _takePhotoColor;

    [ObservableProperty]
    Color? _sendMessageColor;

    public ObservableCollection<ChatMessage> ChatMessages { get; set; } = [];

    private void GetColors()
    {
        if (App.Current!.Resources.TryGetValue("Primary", out object? primaryColor))
            _primaryColor = (Color)primaryColor;

        if (App.Current!.Resources.TryGetValue("Secondary", out object? secondaryColor))
            _secondaryColor = (Color)secondaryColor;
    }

    private void InitChatMessages()
    {
        ChatMessages.Add(new ChatMessage()
        {
            Type = Type.Sent,
            Author = "You",
            Text = "This is a sample sent message."
        });

        ChatMessages.Add(new ChatMessage()
        {
            Type = Type.Received,
            Author = "Echo",
            Text = "This is a sample received message."
        });

        ChatMessages.Add(new ChatMessage()
        {
            Type = Type.System,
            Text = "This is a sample system message."
        });

        ChatMessages.Add(new ChatMessage()
        {
            Type = Type.Sent,
            Author = "You",
            Text = "This is a little bit longer sample sent message to see how multiple lines will look like.\n" +
                   ".NET MAUI is awesome!"
        });

        ChatMessages.Add(new ChatMessage()
        {
            Type = Type.Received,
            Author = "Echo",
            Text = "This is a little bit longer sample sent message to see how multiple lines will look like.\n" +
                   ".NET MAUI is awesome!"
        });
    }

    [RelayCommand]
    async Task SendMessageAsync()
    {
        ChatMessages.Add(new ChatMessage()
        {
            Type = Type.Sent,
            Author = "You",
            Text = UserMessage
        });

        UserMessage = null;

        Status = "Echo is typing...";
        IsStatusVisible = true;
        ToggleColor(SendMessageColor);

        await Task.Delay(1000);

        Status = string.Empty;
        IsStatusVisible = false;
        ToggleColor(SendMessageColor);

        ChatMessages.Add(new ChatMessage()
        {
            Type = Type.Received,
            Author = "Echo",
            Text = $"Echo: {ChatMessages.Last().Text}"
        });
    }

    [RelayCommand]
    void StartStopRecordToggle() => StartStopRecordToggleColor = ToggleColor(StartStopRecordToggleColor);

    [RelayCommand]
    void HandsFreeModeToggle() => HandsFreeModeToggleColor = ToggleColor(HandsFreeModeToggleColor);

    [RelayCommand]
    void AddAttachment() { }

    [RelayCommand]
    void TakePhoto() { }

    private Color? ToggleColor(Color? color)
    {
        if (color == _primaryColor)
            color = _secondaryColor;
        else
            color = _primaryColor;

        return color;
    }
}
