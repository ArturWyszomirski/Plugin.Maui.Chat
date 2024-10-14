namespace Plugin.Maui.Chat.Sample.ViewModels;

public partial class CustomizedChatViewModel : ObservableRecipient
{
    public CustomizedChatViewModel(IAudioService audioService, ITextToSpeechService textToSpeechService)
    {
        AudioService = audioService;
        TextToSpeechService = textToSpeechService;

        InitChatMessages();
    }

    [ObservableProperty]
    IAudioService audioService;

    [ObservableProperty]
    ITextToSpeechService textToSpeechService;

    public ObservableCollection<ChatMessage> ChatMessages { get; set; } = [];

    [ObservableProperty]
    string? textContent;

    [ObservableProperty]
    object? audioContent;

    [ObservableProperty]
    string? status;

    [ObservableProperty]
    bool isStatusVisible = true;

    [ObservableProperty]
    bool isHandsFreeMode;

    private void InitChatMessages()
    {
        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Sent,
            Author = "You",
            TextContent = "This is a sent message sample."
        });

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Received,
            Author = "Echo",
            TextContent = "This is a received message sample."
        });

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.System,
            TextContent = "This is a system message sample."
        });

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Sent,
            Author = "You",
            TextContent = "This is a little bit longer sent message sample to see how multiple lines look like.",
            AudioContent = new EmptyAudioSource() // TODO: add some audio content
        });

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Received,
            Author = "Echo",
            TextContent = "This is a little bit longer received message sample to see how multiple lines look like.",
            AudioContent = new EmptyAudioSource() // TODO: add some audio content
        });
    }

    [RelayCommand]
    async Task SendMessageAsync()
    {
        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Sent,
            Author = "You",
            TextContent = TextContent,
            AudioContent = AudioContent
        });

        TextContent = null;
        AudioContent = null;

        Status = "Echo is messaging...";
        IsStatusVisible = true;

        await Task.Delay(1000);

        Status = string.Empty;
        IsStatusVisible = false;

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Received,
            Author = "Echo",
            TextContent = $"Echo: {ChatMessages.Last().TextContent}",
            AudioContent = ChatMessages.Last().AudioContent
        });
    }

    [RelayCommand]
    void HandsFreeMode() => IsHandsFreeMode = !IsHandsFreeMode;

    [RelayCommand]
    void AddAttachment() { }

    [RelayCommand]
    void TakePhoto() { }
}
