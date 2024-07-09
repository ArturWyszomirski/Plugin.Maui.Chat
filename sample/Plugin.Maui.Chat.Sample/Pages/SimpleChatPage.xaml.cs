namespace Plugin.Maui.Chat.Sample.Pages;

public partial class SimpleChatPage : ContentPage
{
	public SimpleChatPage(SimpleChatViewModel simpleChatViewModel)
	{
		InitializeComponent();

		BindingContext = simpleChatViewModel;
	}
}