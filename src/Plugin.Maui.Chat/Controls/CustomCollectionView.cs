namespace Plugin.Maui.Chat.Controls;

public class CustomCollectionView : CollectionView
{

    public void TurnOnScrollToLastItem()
    {
#if !WINDOWS
        this.ItemsUpdatingScrollMode = ItemsUpdatingScrollMode.KeepLastItemInView;
#else

        // TODO
#endif
    }
    
    public void TurnOffScrollToLastItem()
    {
#if !WINDOWS
        this.ItemsUpdatingScrollMode = ItemsUpdatingScrollMode.KeepScrollOffset;
#else
        /// TODO
#endif

    }
}