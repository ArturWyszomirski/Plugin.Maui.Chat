![nuget.png](https://raw.githubusercontent.com/ArturWyszomirski/Plugin.Maui.Chat/main/nuget.png)
# Plugin.Maui.Chat

`Plugin.Maui.Chat` provides highly customizable chat control providing text and voice messaging. 

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
- Collection of messages of `ChatMessages` type.
- User message entry field with buttons attached.

`ChatMessage` properties:
- `DateTime` which is getter only and provides the date and time at which `ChatMessage` was created. The visibility of message timestamp can be set by related property (see: [Sent and received messages](https://github.com/ArturWyszomirski/Plugin.Maui.Chat?tab=readme-ov-file#sent-and-received-messages));
- `Type` of `MessageType` which can be `Sent` (written by user), `Received` (sent to the user) or `System` (informational type).
- `Author` of string type is the author of the message. The visibility of message author can be set by related property (see: [Sent and received messages](https://github.com/ArturWyszomirski/Plugin.Maui.Chat?tab=readme-ov-file#sent-and-received-messages));
- `TextContent` is message's text content
- `AudioContent` is message's audio content

The send button is by default disabled if the field is empty or consist only whitespace characters.

Visibility of other buttons in the user message entry field as well as their's icons, colors and behaviors can be easily switched on or off (see: [Customized usage](https://github.com/ArturWyszomirski/Plugin.Maui.Chat?tab=readme-ov-file#customized-usage)).

> [!NOTE]
> Below examples assumes using MVVM architecture with ViewModel binded to the XAML page.

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

> [!NOTE]
> By default colors corresponds to those set in `Resources\Styles\Colors.xaml`, but can be easily changed (see: [Customized usage](https://github.com/ArturWyszomirski/Plugin.Maui.Chat?tab=readme-ov-file#customized-usage)).

#### Text messaging

<a href="./assets/screenshots/simple_chat_01.png">
  <img src="./assets/screenshots/simple_chat_01.png" alt="Thumbnail" width="200">
</a>

All you have to do to get started is to deal with those three properties:
- `TextContent` of string type which holds the user input,
- `ChatMessages` which is a collection of `ChatMessage`,
- `SendMessageCommand` where you decide what happens after firing Send message button.

Example below shows how to bind properties. In this scenario every sent message will be repeated and send back after 1 second.

View:
```xaml
<chat:Chat TextContent="{Binding TextContent}"
           ChatMessages="{Binding ChatMessages}"
           SendMessageCommand="{Binding SendMessageCommand}"
           Margin="10"/>
```

ViewModel:
```csharp
[ObservableProperty]
string? textContent;

public ObservableCollection<ChatMessage> ChatMessages { get; set; } = [];

[RelayCommand]
async Task SendMessageAsync()
{
    ChatMessages.Add(new ChatMessage()
    {
        Type = MessageType.Sent,
        Author = "You",
        Text = TextContent
    });

    TextContent = null;

    await Task.Delay(1000);

    ChatMessages.Add(new ChatMessage()
    {
        Type = MessageType.Received,
        Author = "Echo",
        Text = $"Echo: {ChatMessages.Last().Text}"
    });
}
```

#### Voice messaging

> [!NOTE]
> To use voice messaging setting relevant permissions to record audio is necessary.

`Chat` provides built-in functionality for voice messaging based on `Plugin.Maui.Audio`. Audio Recorder is equipped in silence detection, so recording should stop automatically when user stops speaking.

> [!WARNING]
> Silence detection is not yet available on iOS and MacOS.

In order to add voice messaging capability, just turn on Audio Recorder button visibility and add a property that will hold recorded audio content.

View:
```xaml
IsAudioRecorderVisible="True"
AudioContent="{Binding AudioContent}"
```

ViewModel:
```csharp
[ObservableProperty]
object? audioContent;
```

> [!NOTE]
> Audio Recorder icon, color and behavior can be changed as shown in [Audio recorder button chapter](https://github.com/ArturWyszomirski/Plugin.Maui.Chat?tab=readme-ov-file#audio-recorder-button)

When recording is finished the Audio Player icon will pop up in the user message frame. Tap on it to listen to the recorder content.

> [!NOTE]
> Audio Player icon, color and behavior can be changed as shown in [Audio player button chapter](https://github.com/ArturWyszomirski/Plugin.Maui.Chat?tab=readme-ov-file#audio-player-button)

Audio player will show up in every received and send message where `AudioContent` is not null.

> [!NOTE]
> Built-in methods can also be changed to custom ones. In order to do that bind your own method to Audio Recorder or Player command property.

##### Text-to-speech

In order to turn on transcription set `IsSpeechToTextEnabled="True"`. Then, instead of generating `AudioConent`, the voice will be converted to text and added to `TextContent`.

##### Speech-to-text

Setting `IsTextReaderVisible="True"` will result in showing up a text reader icon in every received message. 

> [!NOTE]
> This functionality utilize native text-to-speech converter, but you're able to change this by setting `TextReaderCommand`. As always icon and its color can also be changed.

##### Hands-free mode

Hands-free mode is especially useful when using chat to communicate with bots. The idea is to loop those three steps:
- transcribe user's speech to text,
- send text, 
- read received answer.

The loop breaks when: user stops speaking and there is nothing to transcribe, speech-to-text was for some reason unsuccessful or user manually turns off hands-free mode.

This kind of functionality is strongly bound to send method and therefore it can't be delivered as a built-in feature. However to give en example how it could be implemented, there is a sample prepared. This sample uses Chat's `SpeechToTextService` and `TextToSpeechService` which are exposed in properties with the same names. Binding to those properties enables using native transcription and read methods.

### Customized usage

Many UI elements apperance can be easily changed as well as methods bound to commands in buttons. Below you will find code snippets for each section of `Chat` control and some descriptions.

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
SentMessageAudioContentColor="{StaticResource Secondary}"

ReceivedMessageBackgroundColor="{StaticResource Secondary}"
IsReceivedMessageAuthorVisible="True"
ReceivedMessageAuthorTextColor="{StaticResource Tertiary}"
IsReceivedMessageTimestampVisible="True"
ReceivedMessageTimestampTextColor="{StaticResource Tertiary}"
ReceivedMessageContentTextColor="{StaticResource Primary}"
ReceivedMessageAudioContentColor="{StaticResource Primary}"
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

All buttons expose properties that enable changing icons, colors, visibilty and commands.

##### Audio recorder button

By default audio recorder button is bound to `Plugin.Maui.Audio` recoder using a silence detection which will automatically stop recording when user stop speaking. While recording the button will turn red.

```xaml
AudioRecorderCommand="{Binding AudioRecorderCommand}"
IsAudioRecorderVisible="True"
AudioRecorderIcon="{Static resources:Icons.Microphone}"
AudioRecorderColor="{StaticResource Primary}"
```

##### Audio player button

Whenever `AudioContent` in message is not null an audio player will pop up using `Plugin.Maui.Audio` to enable playing voice messages. While playing record the icon will turn green. 

```xaml
AudioContent="{Binding AudioContent}"
AudioPlayerCommand="{Binding PlayAudioCommand}"
AudioContentIcon="{Static resources:Icons.Waveform}"
AudioContentColor="{StaticResource Primary}"
```

##### Text reader button

By default text reader is not visible. To turn it on set `IsTextReaderVisible="True"`. Text reader uses framework's native text-to-speech, but you are permised to change it binding your own method to `TextReaderCommand` property.

```xaml
TextReaderCommand="{Binding ReadMessageCommand}"
IsTextReaderVisible="True"
TextReaderColor="{StaticResource Tertiary}"
TextReaderIcon="{Static resources:Icons.Speaker}"
```

##### Hands-free mode button

By default hand-free mode is not visible and there is no method bound to its command. As this functionality is strictly bound to send method, you have to implement it by yourself. For more information, check out [Hands-free mode chapter](https://github.com/ArturWyszomirski/Plugin.Maui.Chat?tab=readme-ov-file#hands-free-mode)

```xaml
HandsFreeModeCommand="{Binding HandsFreeModeCommand}"
IsHandsFreeModeVisible="True"
HandsFreeModeIcon="{Static resources:Icons.Headphones}"
HandsFreeModeColor="{StaticResource Primary}"
```

##### Take photo button

```xaml
AddAttachmentCommand="{Binding AddAttachmentCommand}"
IsAddAttachmentVisible="True"
AddAttachmentIcon="{Static resources:Icons.PaperClip}"
AddAttachmentColor="{StaticResource Primary}"
```

##### Add attachment button

```xaml
TakePhotoCommand="{Binding TakePhotoCommand}"
IsTakePhotoVisible="True"
TakePhotoIcon="{Static resources:Icons.Camera}"
TakePhotoColor="{StaticResource Primary}"
```

##### Send message button

By default button is disabled when user text entry is empty or consist only whitespace characters and no other content is present in the message.

> [!NOTE]
> This behavior can be changed by setting the `IsSendMessageEnabled` property.

```xaml
SendMessageCommand="{Binding SendMessageCommand}"
IsSendMessageVisible="True"
SendMessageIcon="{Static resources:Icons.PaperPlane}"
SendMessageColor="{StaticResource Primary}"
```

### Services

Services used by `Chat` are exposed through relevant properties. Below you'll find list of services and available methods.

#### `AudioService`: 
- `StartOrStopRecorderAsync` - start or stops audio recording depending on `IsRecording` property. Method uses silence detection and will autonomously stops recording when user stop speaking. Methods return recording as `IAudioSource`.
- `StartStopPlayerAsync(IAudioSource? audioSource)` - start or stops playing audio depending on `IsPlaying` property.

#### `SpeechToTextService`:
- `StartOrStopTranscriptionAsync` - start or stops speech-to-text conversion depending on `IsRecording` property. Method uses silence detection and will autonomously stops recording when user stop speaking. Methods return transcription as `string`.
- `CancelSpeechToTextTokenSource` - use this token source to manually turn-off transcription. You'll find usage example in Hands-free mode sample.

#### `TextToSpeechService`:
- `StartOrStopReadAsync(string? text)` - start or stops text-to-speech conversion depending on `IsReading` property. 
- `CancelReadingTokenSource` - use this token source to manually turn-off reading. You'll find usage example in Hands-free mode sample.

## Credits

### Icons

All icons comes from [flaticon.com](https://www.flaticon.com)'s UICONS series.

Icon was coloured using https://onlinepngtools.com/change-png-color and converted to svg using https://convertio.co

### Music

"The Happy Ukulele Song" by Stanislav Fomin