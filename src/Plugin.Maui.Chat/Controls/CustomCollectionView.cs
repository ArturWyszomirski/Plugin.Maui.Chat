namespace Plugin.Maui.Chat.Controls;

public class CustomCollectionView : CollectionView
{

    public void TurnOnScrollToLastItem()
    {
        this.ItemsUpdatingScrollMode = ItemsUpdatingScrollMode.KeepLastItemInView;
    }
    
    public void TurnOffScrollToLastItem()
    {
        this.ItemsUpdatingScrollMode = ItemsUpdatingScrollMode.KeepScrollOffset;
    }
}