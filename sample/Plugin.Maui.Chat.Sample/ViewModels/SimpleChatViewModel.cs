namespace Plugin.Maui.Chat.Sample.ViewModels;

public partial class SimpleChatViewModel : ObservableRecipient
{
    public SimpleChatViewModel()
    {
        InitChatMessages();
    }

    [ObservableProperty]
    string? _userMessage;

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

        await Task.Delay(1000);

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Received,
            Author = "Echo",
            Text = $"Echo: {ChatMessages.Last().Text}"
        });
    }
}
