namespace Plugin.Maui.Chat.Sample.ViewModels;

public partial class SimpleChatViewModel : ObservableRecipient
{
    public SimpleChatViewModel()
    {
        InitChatMessages();
    }

    [ObservableProperty]
    string? textContent;

    public ObservableCollection<ChatMessage> ChatMessages { get; set; } = [];

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
            TextContent = "This is a little bit longer sent message sample to see how multiple lines look like."
        });

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Received,
            Author = "Echo",
            TextContent = "This is a little bit longer received message sample to see how multiple lines look like."
        });
    }

    [RelayCommand]
    async Task SendMessageAsync()
    {
        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Sent,
            Author = "You",
            TextContent = TextContent
        });

        TextContent = null;

        await Task.Delay(1000);

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Received,
            Author = "Echo",
            TextContent = $"Echo: {ChatMessages.Last().TextContent}"
        });
    }
}
