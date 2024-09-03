﻿using System.ComponentModel;

namespace Plugin.Maui.Chat.Behaviors;

/// <summary>
/// Disables send message button if user message is empty or white space.
/// </summary>
internal class UserMessageValidationBehavior : Behavior<Controls.Chat>
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
        var chat = sender as Controls.Chat ?? throw new ArgumentNullException(nameof(sender));

        if (e.PropertyName == Controls.Chat.UserMessageProperty.PropertyName)
            chat.IsSendMessageEnabled = !string.IsNullOrWhiteSpace(chat?.UserMessage);
    }
}
