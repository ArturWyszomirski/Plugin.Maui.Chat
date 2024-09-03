namespace Plugin.Maui.Chat.Sample.ViewModels;

public partial class CustomizedChatViewModel : ObservableRecipient
{
    public CustomizedChatViewModel()
    {
        InitChatMessages();
    }

    [ObservableProperty]
    string? _userMessage;

    [ObservableProperty]
    string? _status;

    [ObservableProperty]
    bool _isStatusVisible;

    [ObservableProperty]
    bool _isRecording;

    [ObservableProperty]
    bool _isHandsFreeMode;

    public ObservableCollection<ChatMessage> ChatMessages { get; set; } = [];

    private void InitChatMessages()
    {
        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Sent,
            Author = "You",
            Text = "This is a sample sent message."
        });

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Received,
            Author = "Echo",
            Text = "This is a sample received message."
        });

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.System,
            Text = "This is a sample system message."
        });

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Sent,
            Author = "You",
            Text = "This is a little bit longer sample sent message to see how multiple lines will look like.\n" +
                   ".NET MAUI is awesome!"
        });

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Received,
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
            Type = MessageType.Sent,
            Author = "You",
            Text = UserMessage
        });

        UserMessage = null;

        Status = "Echo is typing...";
        IsStatusVisible = true;

        await Task.Delay(1000);

        Status = string.Empty;
        IsStatusVisible = false;

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Received,
            Author = "Echo",
            Text = $"Echo: {ChatMessages.Last().Text}"
        });
    }

    [RelayCommand]
    void StartStopRecordToggle() => IsRecording = !IsRecording;

    [RelayCommand]
    void HandsFreeModeToggle() => IsHandsFreeMode = !IsHandsFreeMode;

    [RelayCommand]
    void AddAttachment() { }

    [RelayCommand]
    void TakePhoto() { }
}
