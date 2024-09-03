namespace Plugin.Maui.Chat.Helpers;

internal class ChatMessageTemplateSelector : DataTemplateSelector
{
    public DataTemplate? SentMessage { get; set; }
    public DataTemplate? ReceivedMessage { get; set; }
    public DataTemplate? SystemMessage { get; set; }

    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
    {
        if (item is ChatMessage message)
        {
            return message.Type switch
            {
                MessageType.Sent => SentMessage,
                MessageType.Received => ReceivedMessage,
                MessageType.System => SystemMessage,
                _ => throw new ArgumentException("Invalid item value", nameof(item))
            };
        }
        else
            throw new ArgumentException("Invalid item type", nameof(item));
    }
}