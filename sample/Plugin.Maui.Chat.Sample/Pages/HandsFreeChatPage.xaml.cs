namespace Plugin.Maui.Chat.Sample.Pages;

public partial class HandsFreeChatPage : ContentPage
{
	public HandsFreeChatPage(HandsFreeChatViewModel handsFreeChatViewModel)
	{
		InitializeComponent();

		BindingContext = handsFreeChatViewModel;
	}
}