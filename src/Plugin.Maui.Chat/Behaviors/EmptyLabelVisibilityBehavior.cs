using System.ComponentModel;

namespace Plugin.Maui.Chat.Behaviors;

/// <summary>
/// Hides label if empty.
/// </summary>
internal class EmptyLabelVisibilityBehavior : Behavior<Label>
{
    protected override void OnAttachedTo(BindableObject bindable)
    {
        bindable.PropertyChanged += OnPropertyChanged;
        base.OnAttachedTo(bindable);
    }

    protected override void OnDetachingFrom(BindableObject bindable)
    {
        bindable.PropertyChanged -= OnPropertyChanged;
        base.OnDetachingFrom(bindable);
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var label = sender as Label ?? throw new ArgumentNullException(nameof(sender));

        if (string.IsNullOrEmpty(label.Text))
            label.IsVisible = false;
        else
            label.IsVisible = true;
    }
}
