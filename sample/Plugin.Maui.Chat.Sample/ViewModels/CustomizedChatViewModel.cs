namespace Plugin.Maui.Chat.Sample.ViewModels;

public partial class CustomizedChatViewModel : ObservableRecipient
{
    public CustomizedChatViewModel()
    {
        InitChatMessages();
    }

    [ObservableProperty]
    string? textContent;

    [ObservableProperty]
    string? status;

    [ObservableProperty]
    bool isStatusVisible;

    [ObservableProperty]
    bool isRecording;

    [ObservableProperty]
    bool isHandsFreeMode;

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
            TextContent = TextContent
        });

        TextContent = null;

        Status = "Echo is typing...";
        IsStatusVisible = true;

        await Task.Delay(1000);

        Status = string.Empty;
        IsStatusVisible = false;

        ChatMessages.Add(new ChatMessage()
        {
            Type = MessageType.Received,
            Author = "Echo",
            TextContent = $"Echo: {ChatMessages.Last().TextContent}"
        });
    }

    [RelayCommand]
    async Task StartStopRecordingAsync()
    { 
        IsRecording = !IsRecording;

        if (IsRecording)
            await Shell.Current.DisplayAlert("Custom record audio command", "You can use your own commands instead of those built-in.", "Cool :)");
    }

    [RelayCommand]
    async Task PlayAudioAsync()
    {
        var audioPlayer = AudioManager.Current.CreateAsyncPlayer(await FileSystem.OpenAppPackageFileAsync("ukulele.mp3"));

        await Shell.Current.DisplayAlert("Custom play audio command",
                                         "On this page all audio contents will play \"The Happy Ukulele Song\" by Stanislav Fomin. Enjoy!",
                                         "Aloha :)");

        await audioPlayer.PlayAsync(default);
    }

    [RelayCommand]
    void HandsFreeMode() => IsHandsFreeMode = !IsHandsFreeMode;

    [RelayCommand]
    void AddAttachment() { }

    [RelayCommand]
    void TakePhoto() { }
}
