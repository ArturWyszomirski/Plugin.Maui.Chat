namespace Plugin.Maui.Chat.Sample.Pages;

public partial class TranscriptionChatPage : ContentPage
{
	public TranscriptionChatPage(TranscriptionChatViewModel transcriptionChatViewModel)
	{
		InitializeComponent();

		BindingContext = transcriptionChatViewModel;
	}
}