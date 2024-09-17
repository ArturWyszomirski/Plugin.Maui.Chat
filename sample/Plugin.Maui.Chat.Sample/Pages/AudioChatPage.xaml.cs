namespace Plugin.Maui.Chat.Sample.Pages;

public partial class AudioChatPage : ContentPage
{
	public AudioChatPage(AudioChatViewModel simpleChatViewModel)
	{
		InitializeComponent();

		BindingContext = simpleChatViewModel;
	}
}