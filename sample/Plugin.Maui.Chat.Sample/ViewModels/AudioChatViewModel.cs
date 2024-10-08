﻿namespace Plugin.Maui.Chat.Sample.ViewModels;

public partial class AudioChatViewModel : ObservableRecipient
{
    public AudioChatViewModel()
    {
        InitChatMessages();
    }
    public ObservableCollection<ChatMessage> ChatMessages { get; } = [];

    [ObservableProperty]
    string? textContent;

    [ObservableProperty]
    object? audioContent;

    [ObservableProperty]
    string? status;

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
            TextContent = TextContent,
            AudioContent = AudioContent
        });

        TextContent = null;
        AudioContent = null;

        Status = "Echo is messaging...";

        await Task.Delay(1000);

        Status = string.Empty;

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Received,
            Author = "Echo",
            TextContent = !string.IsNullOrEmpty(ChatMessages.Last().TextContent) ? $"Echo: {ChatMessages.Last().TextContent}" : string.Empty,
            AudioContent = ChatMessages.Last().AudioContent
        });
    }
}
