namespace Plugin.Maui.Chat.Controls;

public class CustomCollectionView : CollectionView
{
    public void TurnOnScrollToLastItem()
    {
        
        //https://github.com/dotnet/maui/issues/17369
#if !WINDOWS   
        this.ItemsUpdatingScrollMode = ItemsUpdatingScrollMode.KeepLastItemInView;
#else

        // TODO
#endif
    }
    
    public void TurnOffScrollToLastItem()
    {
            //https://github.com/dotnet/maui/issues/17369
#if !WINDOWS
        this.ItemsUpdatingScrollMode = ItemsUpdatingScrollMode.KeepScrollOffset;
#else
        /// TODO
#endif

    }
}