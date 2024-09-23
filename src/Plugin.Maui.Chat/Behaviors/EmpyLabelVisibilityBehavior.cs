using System.ComponentModel;

namespace Plugin.Maui.Chat.Behaviors;

/// <summary>
/// Disables send message button if user message is empty or white space.
/// </summary>
internal class EmpyLabelVisibilityBehavior : Behavior<Label>
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
