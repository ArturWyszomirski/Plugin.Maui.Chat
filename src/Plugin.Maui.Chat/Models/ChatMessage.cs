namespace Plugin.Maui.Chat.Models;

public class ChatMessage
{
    public DateTime DateTime { get; } = DateTime.Now;
    public MessageType Type { get; set; }
    public string? Author { get; set; }
    public string? TextContent { get; set; }
    public object? AudioContent { get; set; }
}