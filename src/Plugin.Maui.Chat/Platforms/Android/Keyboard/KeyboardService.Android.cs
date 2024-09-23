using Android.App;
using AndroidX.Core.View;
using Plugin.Maui.Chat.Helpers;
using Plugin.Maui.Chat.Services;
using View = Android.Views.View;

namespace Plugin.Maui.Chat.Keyboard;

public class KeyboardService : IKeyboardService
{
    private readonly Activity _activity;
    private View _rootView;
    private IOnApplyWindowInsetsListener _insetsListener;
    public event EventHandler<KeyboardEventArgs> KeyboardHeightChanged;

    public KeyboardService()
    {
        _activity = Platform.CurrentActivity;
        _rootView = _activity.Window.DecorView.RootView;
    }

    public void Start()
    {
        _insetsListener = new WindowInsetsListener(this);
        ViewCompat.SetOnApplyWindowInsetsListener(_rootView, _insetsListener);
    }

    public void Stop()
    {
        if (_insetsListener != null)
        {
            ViewCompat.SetOnApplyWindowInsetsListener(_rootView, null);
            _insetsListener = null;
        }
    }

    private class WindowInsetsListener : Java.Lang.Object, IOnApplyWindowInsetsListener
    {
        private readonly KeyboardService _keyboardService;

        public WindowInsetsListener(KeyboardService keyboardService)
        {
            _keyboardService = keyboardService;
        }

        public WindowInsetsCompat OnApplyWindowInsets(View v, WindowInsetsCompat insets)
        {
            var imeVisible = insets.IsVisible(WindowInsetsCompat.Type.Ime());

            _keyboardService.KeyboardHeightChanged?.Invoke(_keyboardService, new KeyboardEventArgs
            {
                KeyboardHeight = imeVisible ? 200 : 0
            });

            return insets;
        }
    }
}