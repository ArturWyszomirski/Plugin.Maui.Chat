namespace Plugin.Maui.Chat.Sample.ViewModels;

public partial class AudioChatViewModel : ObservableRecipient
{
    public AudioChatViewModel()
    {
        InitChatMessages();
    }

    [ObservableProperty]
    string? userMessage;

    [ObservableProperty]
    IAudioSource? audioContent;

    public ObservableCollection<ChatMessage> ChatMessages { get; set; } = [];

    private void InitChatMessages()
    {
        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Sent,
            Author = "You",
            Text = "This is a sent message sample."
        });

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Received,
            Author = "Echo",
            Text = "This is a received message sample."
        });

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.System,
            Text = "This is a system message sample."
        });

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Sent,
            Author = "You",
            Text = "This is a little bit longer sent message sample to see how multiple lines look like."
        });

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Received,
            Author = "Echo",
            Text = "This is a little bit longer received message sample to see how multiple lines look like."
        });
    }

    [RelayCommand]
    async Task SendMessageAsync()
    {
        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Sent,
            Author = "You",
            Text = UserMessage,
            AudioContent = AudioContent
        });

        UserMessage = null;
        AudioContent = null;

        await Task.Delay(1000);

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Received,
            Author = "Echo",
            Text = !string.IsNullOrEmpty(ChatMessages.Last().Text) ? $"Echo: {ChatMessages.Last().Text}" : string.Empty,
            AudioContent = ChatMessages.Last().AudioContent
        });
    }
}
