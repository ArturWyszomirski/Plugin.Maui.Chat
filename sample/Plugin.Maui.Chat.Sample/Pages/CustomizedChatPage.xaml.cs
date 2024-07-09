namespace Plugin.Maui.Chat.Sample.Pages;

public partial class CustomizedChatPage : ContentPage
{
	public CustomizedChatPage(CustomizedChatViewModel customizedChatViewModel)
	{
		InitializeComponent();

		BindingContext = customizedChatViewModel;
	}
}