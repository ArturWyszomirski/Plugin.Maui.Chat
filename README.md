![nuget.png](https://raw.githubusercontent.com/ArturWyszomirski/Plugin.Maui.Chat/main/nuget.png)
# Plugin.Maui.Chat

`Plugin.Maui.Chat` provides highly customizable chat control. By default colors corresponds to those set in `Resources\Styles\Colors.xaml`, but can be easily changed (see: [Customized usage](https://github.com/ArturWyszomirski/Plugin.Maui.Chat/edit/Documentation/README.md#customized-usage)).

As for now this control enables only text messaging but soon it will be upgraded with some additional features like voice messages, speech-to-text transcription, text-to-speech converter and much more:)

> [!NOTE]
> The UI was tested only on Android. In near future the Windows version will also be polished.

## Install Plugin

[![NuGet](https://img.shields.io/nuget/v/Plugin.Maui.Chat.svg?label=NuGet)](https://www.nuget.org/packages/Plugin.Maui.Chat/)

Available on [NuGet](http://www.nuget.org/packages/Plugin.Maui.Chat).

Install with the dotnet CLI: `dotnet add package Plugin.Maui.Chat`, or through the NuGet Package Manager in Visual Studio.

### Supported Platforms

| Platform | Minimum Version Supported |
|----------|---------------------------|
| iOS      | 11+                       |
| macOS    | 10.15+                    |
| Android  | 5.0 (API 21)              |
| Windows  | 11 and 10 version 1809+   |

## API Usage

`Chat` control may be roughly divided in two fields:
- the collection of messages of `ChatMessages` type;
- the user message entry field with buttons attached.

`ChatMessage` consist four properties:
- `DateTime` which is getter only and provides the date and time at which `ChatMessage` was created. The visibility of message timestamp can be set by related property (see: [Sent and received messages](https://github.com/ArturWyszomirski/Plugin.Maui.Chat/edit/Documentation/README.md#sent-and-received-messages));
- `Type` of `MessageType` which can be `Sent` (written by user), `Received` (sent to the user) or `System` (informational type).
- `Author` of string type is the author of the message. The visibility of message author can be set by related property (see: [Sent and received messages](https://github.com/ArturWyszomirski/Plugin.Maui.Chat/edit/Documentation/README.md#sent-and-received-messages));
- `Text` is message's content

The send button is by default disabled if the field is empty or consist only whitespace characters.

Visibility of other buttons in the user message entry field as well as their's icons, colors and behaviors can be easily switched on or off. See: Customized usage.

Below examples assumes using MVVM architecture with ViewModel binded to the XAML page.

### Dependency Injection

This NuGet depends on `MAUI Community Toolkit`, so you will first need to register the `Feature` with the `MAUI Community Toolkit`.

```csharp
builder.UseMauiCommunityToolkit();
```

### XAML

To use `Chat` you need to register `Plugin.Maui.Chat.Controls` namespace by adding below line to XAML file opening tag.

> [!WARNING]
> Make sure you are adding `Plugin.Maui.Chat.Controls` namespace, not the `Plugin.Maui.Chat`.

```xaml
<ContentPage ...
             xmlns:chat="clr-namespace:Plugin.Maui.Chat.Controls;assembly=Plugin.Maui.Chat"
             ...>
```

### Simple usage

All you have to do to get started is to deal with those three properties:
- `UserMessage` of string type which holds the user input,
- `ChatMessages` which is a collection of `ChatMessage`,
- `SendMessageCommand` where you decide what happens after firing Send message button.

Example below shows how to bind properties. In this scenario every sent message will be repeated and send back after 1 second.

View:
```xaml
<chat:Chat UserMessage="{Binding UserMessage}"
           ChatMessages="{Binding ChatMessages}"
           SendMessageCommand="{Binding SendMessageCommand}"
           Margin="10"/>
```

ViewModel:
```csharp
[ObservableProperty]
string? _userMessage;

public ObservableCollection<ChatMessage> ChatMessages { get; set; } = [];

[RelayCommand]
async Task SendMessageAsync()
{
    ChatMessages.Add(new ChatMessage()
    {
        Type = MessageType.Sent,
        Author = "You",
        Text = UserMessage
    });

    UserMessage = null;

    await Task.Delay(1000);

    ChatMessages.Add(new ChatMessage()
    {
        Type = MessageType.Received,
        Author = "Echo",
        Text = $"Echo: {ChatMessages.Last().Text}"
    });
}
```

### Customized usage

Below you will find code snippets for each section of `Chat` control and some descriptions.

#### Messages

##### Sent and received messages

The properties in both are analogous and related to colors of texts and background as well as visibility of message's author and timestamp fields.

```xaml
SentMessageBackgroundColor="{StaticResource Primary}"
IsSentMessageAuthorVisible="True"
SentMessageAuthorTextColor="{StaticResource Secondary}"
IsSentMessageTimestampVisible="True"
SentMessageTimestampTextColor="{StaticResource Secondary}"
SentMessageContentTextColor="{StaticResource Secondary}"

ReceivedMessageBackgroundColor="{StaticResource Secondary}"
IsReceivedMessageAuthorVisible="True"
ReceivedMessageAuthorTextColor="{StaticResource Tertiary}"
IsReceivedMessageTimestampVisible="True"
ReceivedMessageTimestampTextColor="{StaticResource Tertiary}"
ReceivedMessageContentTextColor="{StaticResource Primary}"
```

##### System message

You can set text and background color.

```xaml
SystemMessageBackgroundColor="{StaticResource Gray200}"
SystemMessageTextColor="{StaticResource Gray900}"
```

#### Status field

Status is a field just above the user entry. The purpose of this field is to inform user about some actions taking place, i.e. "Sandy is typing..."
```xaml
Status="{Binding Status}"
IsStatusVisible="{Binding IsStatusVisible}"
StatusTextColor="{StaticResource Gray500}"
```

`Status` is a string type. `IsStatusVisible` is a bool type. 

#### Buttons

##### Send message

By default button is disabled when user message entry is empty or consist only whitespace characters. This can be changed by setting the `IsSendMessageEnabled` property.

By default button is not visible, but it has already set icon and color. You can easily change button's icon, color, visibility and of course bind the method (command) triggered by pushing the button.

```xaml
SendMessageCommand="{Binding SendMessageCommand}"
IsSendMessageVisible="True"
SendMessageIcon="{Static resources:Icons.PaperPlane}"
SendMessageColor="{StaticResource Primary}"
```

##### Start/stop record toggle, Hands-free mode toggle, Take photo and Add attachment buttons

Analogous to Send message button there are already presetted colors and icon's which can easily be changed by related properties. For now there are no default commands binded to buttons.

```
StartStopRecordToggleCommand="{Binding StartStopRecordToggleCommand}"
IsStartStopRecordToggleVisible="True"
StartStopRecordToggleIcon="{Static resources:Icons.Microphone}"

HandsFreeModeToggleCommand="{Binding HandsFreeModeToggleCommand}"
IsHandsFreeModeToggleVisible="True"
HandsFreeModeToggleIcon="{Static resources:Icons.Headphones}"

AddAttachmentCommand="{Binding AddAttachmentCommand}"
IsAddAttachmentVisible="True"
AddAttachmentIcon="{Static resources:Icons.PaperClip}"
AddAttachmentColor="{StaticResource Primary}"

TakePhotoCommand="{Binding TakePhotoCommand}"
IsTakePhotoVisible="True"
TakePhotoIcon="{Static resources:Icons.Camera}"
TakePhotoColor="{StaticResource Primary}"
```

## Credits

All icons comes from [flaticon.com](https://www.flaticon.com)'s UICONS series.

Icon was coloured using https://onlinepngtools.com/change-png-color and converted to svg using https://convertio.co
