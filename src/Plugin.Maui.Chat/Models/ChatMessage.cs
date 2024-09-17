namespace Plugin.Maui.Chat.Models;

public class ChatMessage
{
    public DateTime DateTime { get; } = DateTime.Now;
    public MessageType Type { get; set; }
    public string? Author { get; set; }
    public string? Text { get; set; }
    public IAudioSource? AudioContent { get; set; }
}