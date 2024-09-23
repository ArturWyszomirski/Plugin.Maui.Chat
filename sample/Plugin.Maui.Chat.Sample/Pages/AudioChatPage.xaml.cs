namespace Plugin.Maui.Chat.Sample.Pages;

public partial class AudioChatPage : ContentPage
{
	public AudioChatPage(AudioChatViewModel audioChatViewModel)
	{
		InitializeComponent();

		BindingContext = audioChatViewModel;
	}
}